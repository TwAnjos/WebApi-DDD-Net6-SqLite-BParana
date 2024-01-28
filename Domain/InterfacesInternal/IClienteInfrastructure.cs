using Domain.Utils.InterfaceGenerics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfacesInternal
{
    public interface IClienteInfrastructure : IGeneric<Cliente>
    {
        Task<List<Cliente>> GetAllClientes(Expression<Func<Cliente,bool>> expression);
        Task<List<Cliente>> GetAllClientesByDDDNumero(string numero);
        Task<List<Cliente>> GetAllClientesById(List<PhoneCliente> phones);
        Task<Cliente> GetByEmailAsync(string emailAntigo);
    }
}
