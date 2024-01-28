using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfacesInternal.InterfacesServices
{
    public interface IServiceCliente
    {
        Task<bool> AtualizarClienteFull(Cliente cliente);
        Task<bool> AtualizarEmailCliente(string emailAntigo, string emailNovo);
        Task<bool> AtualizarTelefoneCliente(int clientId, int telefoneIdAntigo, PhoneCliente phoneClienteNovo);
        bool CadastrarCliente(Cliente cliente);
        Task<bool> DeleteClienteByEmail(string email);
        Task<List<Cliente>> GetAllClientes();
        Task<List<Cliente>> GetAllClientesByDDDNumero(string numero);
    }
}
