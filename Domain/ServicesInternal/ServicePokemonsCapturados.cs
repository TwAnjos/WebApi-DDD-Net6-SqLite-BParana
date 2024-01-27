﻿using Domain.Interfaces;
using Domain.Interfaces.InterfacesServices;
using Entities.Entities;

namespace Domain.Services
{
    public class ServicePokemonsCapturados : IServicePokemonsCapturados
    {
        private readonly IPokemonsCapturadosInfrastructure _IPokemonsCapturadosInfrastructure;

        public ServicePokemonsCapturados(IPokemonsCapturadosInfrastructure iPokemonsCapturadosInfrastructure)
        {
            _IPokemonsCapturadosInfrastructure = iPokemonsCapturadosInfrastructure;
        }

        public async Task Adicionar(PokemonsCapturados pokemonsCapturados)
        {
            bool validaTitulo = pokemonsCapturados.ValidarPropriedadeiNT(pokemonsCapturados.PokemonId, "PokemonId");
            if (validaTitulo)
            {
                pokemonsCapturados.DataCapturado = DateTime.Now;
                pokemonsCapturados.DataAlteracao = DateTime.Now;
                pokemonsCapturados.Ativo = true;
                await _IPokemonsCapturadosInfrastructure.Add(pokemonsCapturados);
            }
            else
            {
                throw new Exception("Erro ao salvar pokemon capturado.");
            }
        }

        public async Task Atualizar(PokemonsCapturados pokemonsCapturados)
        {
            bool validaTitulo = pokemonsCapturados.ValidarPropriedadeiNT(pokemonsCapturados.PokemonId, "PokemonId");
            if (validaTitulo)
            {
                pokemonsCapturados.DataAlteracao = DateTime.Now;
                await _IPokemonsCapturadosInfrastructure.Add(pokemonsCapturados);
            }
        }

        public PokemonsCapturados GetPokemonByName(string pokemonName)
        {
            return _IPokemonsCapturadosInfrastructure.GetByName(pokemonName);
        }

        public async Task<List<PokemonsCapturados>> ListarPokemonsCapturadosAtivos(string userId)
        {
            return await _IPokemonsCapturadosInfrastructure.ListarPokemonsCapturados(p => p.Ativo && p.UserId == userId);
        }

        public void RemoveById(PokemonsCapturados pk)
        {
            _IPokemonsCapturadosInfrastructure.Delete(pk);
        }
    }
}