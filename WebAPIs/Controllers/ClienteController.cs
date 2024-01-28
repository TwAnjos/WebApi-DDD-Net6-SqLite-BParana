using Domain.InterfacesInternal.InterfacesServices;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController, Route("api/Files"), Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly IServiceCliente _IserviceCliente;

        public ClienteController(IServiceCliente iserviceCliente)
        {
            _IserviceCliente = iserviceCliente;
        }

        [AllowAnonymous, Produces("application/json"), HttpPost("/api/CadastrarCliente")]
        public IActionResult CadastrarCliente([FromBody] Cliente cliente)
        {
            try
            {
                bool isOk = _IserviceCliente.CadastrarCliente(cliente);

                return Ok($"Cliente cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AllowAnonymous, Produces("application/json"), HttpPost("/api/GetAllClientes")]
        public IActionResult GetAllClientes()
        {
            try
            {
                return Ok(_IserviceCliente.GetAllClientes());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AllowAnonymous, Produces("application/json"), HttpPost("/api/GetAllClientesByDDDNumero")]
        public async Task<IActionResult> GetAllClientesByDDDNumero(string numero)
        {
            try
            {
                return Ok( await _IserviceCliente.GetAllClientesByDDDNumero(numero));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}