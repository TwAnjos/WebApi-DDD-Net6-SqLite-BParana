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

        /// <summary>
        /// Add Cliente
        /// </summary>
        /// <remarks>
        /// 1. Cadastrar o cliente informando o nome completo, e-mail e uma lista de telefones informando o DDD, número e o tipo [fixo ou celular];
        /// </remarks>
        /// <param name="cliente"></param>
        /// <returns>bool</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Listar todos os Clientes
        /// </summary>
        /// <remarks>
        /// 2. Permitir consultar todos os clientes com seus respectivos e-mails e telefones
        /// </remarks>
        /// <returns>
        /// List Cliente
        /// </returns>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpGet("/api/GetAllClientes")]
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

        /// <summary>
        /// Retorna uma lista de objetos de acordo com o DDD ou Numero de telefone.
        /// </summary>
        /// <remarks>
        /// 3. Permitir a consulta de um cliente através do DDD e número;
        /// </remarks>
        /// <param name="numero"></param>
        /// <returns>
        /// List Cliente
        /// </returns>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpGet("/api/GetAllClientesByDDDNumero")]
        public async Task<IActionResult> GetAllClientesByDDDNumero(string numero)
        {
            try
            {
                return Ok(await _IserviceCliente.GetAllClientesByDDDNumero(numero));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza apenas o email do cliente
        /// </summary>
        /// <param name="emailAntigo"></param>
        /// <param name="emailNovo"></param>
        /// <returns></returns>
        /// <remarks>
        /// 4. Permitir a atualização do e-mail do cliente cadastrado;
        /// </remarks>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpPost("/api/AtualizarEmailCliente")]
        public async Task<IActionResult> AtualizarEmailCliente(string emailAntigo, string emailNovo)
        {
            try
            {
                return Ok(await _IserviceCliente.AtualizarEmailCliente(emailAntigo, emailNovo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza apenas um telefone de um Cliente.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="telefoneIdAntigo"></param>
        /// <param name="phoneClienteNovo"></param>
        /// <returns></returns>
        /// <remarks>
        /// 5. Permitir a atualização do telefone do cliente cadastrado;
        /// </remarks>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpPost("/api/AtualizarTelefoneCliente")]
        public async Task<IActionResult> AtualizarTelefoneCliente(int clientId, int telefoneIdAntigo, [FromBody] PhoneCliente phoneClienteNovo)
        {
            try
            {
                return Ok(await _IserviceCliente.AtualizarTelefoneCliente(clientId, telefoneIdAntigo, phoneClienteNovo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove um cliente pelo email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <remarks>
        ///  6. Permitir a exclusão de um cliente através do e-mail.
        ///  </remarks>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpDelete("/api/DeleteClienteByEmail")]
        public async Task<IActionResult> DeleteClienteByEmail(string email)
        {
            try
            {
                return Ok(await _IserviceCliente.DeleteClienteByEmail(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>bool</returns>
        /// <exception cref="Exception"></exception>
        [AllowAnonymous, Produces("application/json"), HttpPost("/api/AtualizarClienteFull")]
        public async Task<IActionResult> AtualizarClienteFull([FromBody] Cliente cliente)
        {
            try
            {
                return Ok(await _IserviceCliente.AtualizarClienteFull(cliente));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}