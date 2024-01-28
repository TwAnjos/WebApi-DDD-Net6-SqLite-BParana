using Domain.InterfacesInternal;
using Domain.InterfacesInternal.InterfacesServices;
using Entities.Entities;

namespace Domain.ServicesInternal
{
    public class ServiceCliente : IServiceCliente
    {
        public readonly IClienteInfrastructure _IclienteInfrastructure;

        public ServiceCliente(IClienteInfrastructure iclienteInfrastructure)
        {
            _IclienteInfrastructure = iclienteInfrastructure;
        }

        public bool CadastrarCliente(Cliente cliente)
        {
            _ = _IclienteInfrastructure.Add(cliente);
            return true;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            return await _IclienteInfrastructure.GetAllClientes(x => x.ClienteId > 0);
        }

        public async Task<List<Cliente>> GetAllClientesByDDDNumero(string ddd)
        {
            var cliente = await _IclienteInfrastructure.GetAllClientesByDDDNumero(ddd);

            return cliente;
        }
    }
}