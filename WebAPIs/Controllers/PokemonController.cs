﻿using AutoMapper;
using Domain.Interfaces.InterfacesServices;
using Domain.InterfacesExternal.InterfacesServices;
using Entities.Entities;
using Entities.EntitiesExternal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class PokemonController : ControllerBase
    {
        //private readonly IMapper _IMapper;
        private readonly IServicePokemonsCapturados _IServicePokemonsCapturados;

        private readonly IServicePokemon _IServicePokemon;

        //public PokemonController(IMapper iMapper, IServicePokemonsCapturados iServicePokemonsCapturados, IServicePokemon iServicePokemon)
        public PokemonController(IServicePokemonsCapturados iServicePokemonsCapturados, IServicePokemon iServicePokemon)
        {
            //_IMapper = iMapper;
            _IServicePokemonsCapturados = iServicePokemonsCapturados;
            _IServicePokemon = iServicePokemon;
        }

        private string RetornaIdUsuarioLogado()
        {
            try
            {
                if (User != null)
                {
                    var idUsuario = User.FindFirst("idUsuario");
                    return idUsuario.Value;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize, Produces("application/json"), HttpGet("/api/List10PokemonRandom"), NonAction]
        public IActionResult List10PokemonRandom()
        {
            List<Pokemon> pokemon = _IServicePokemon.List10PokemonRandom();
            return Ok(pokemon);
        }

        [Authorize, Produces("application/json"), HttpGet("/api/GetPokemonById/{idPokemon}"), NonAction]
        public IActionResult GetPokemonById(int idPokemon)
        {
            Pokemon pokemon = _IServicePokemon.GetPokemonById(idPokemon);
            if (pokemon == null)
            {
                return NotFound("Pokemon não foi encontrado.");
            }
            return Ok(pokemon);
        }

        [Authorize, Produces("application/json"), HttpGet("/api/GetPokemonByName/{GetPokemonByName}"), NonAction]
        public IActionResult GetPokemonByName(string GetPokemonByName)
        {
            Pokemon pokemon = _IServicePokemon.GetPokemonByName(GetPokemonByName);
            if (pokemon == null)
            {
                return NotFound("Pokemon não foi encontrado.");
            }
            return Ok(pokemon);
        }

        [Authorize, Produces("application/json"), HttpPost("/api/CapturarPokemonByNameOrId/{pokemonNameOrId}"), NonAction]
        public async Task<IActionResult> CapturarPokemonByNameOrId(string pokemonNameOrId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pokemonNameOrId))
                {
                    return BadRequest("pokemonNameOrId não pode ser Null Or White Space");
                }

                Pokemon pokemon = _IServicePokemon.GetPokemonByName(pokemonNameOrId);
                if (pokemon is null)
                {
                    return NotFound("Pokemon não foi encontrado.");
                }

                //pega o id do pokemon e salva com o id do usuário
                PokemonsCapturados capturado = new()
                {
                    UserId = RetornaIdUsuarioLogado(),
                    PokemonId = pokemon.id,
                    PokemonName = pokemon.name
                };
                await _IServicePokemonsCapturados.Adicionar(capturado);

                return Ok("Pokemon capturado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao capturar pokemon. "+ex.Message);
            }
        }

        [Authorize, Produces("application/json"), HttpGet("/api/ListarTodosMeusPokemonsCapturados"), NonAction]
        public IActionResult ListarTodosMeusPokemonsCapturados()
        {
            try
            {
                string userId = RetornaIdUsuarioLogado();
                return Ok(_IServicePokemonsCapturados.ListarPokemonsCapturadosAtivos(userId));
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao retornar lista" + ex);
            }
        }

        [Authorize, Produces("application/json"), HttpDelete("/api/RemoverPokemonByName/{pokemonName}"), NonAction]
        public IActionResult RemoverPokemonByName(string pokemonName)
        {
            try
            {
                PokemonsCapturados pk = _IServicePokemonsCapturados.GetPokemonByName(pokemonName);
                if (pk is null)
                {
                    return NotFound("Objeto não encontrado");
                }
                _IServicePokemonsCapturados.RemoveById(pk);

                return Ok($"O Pokemon {pk.PokemonName} foi removido.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao remover pokemon");
            }
        }
    }
}