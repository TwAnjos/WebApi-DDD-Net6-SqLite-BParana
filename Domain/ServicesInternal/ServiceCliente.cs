using Domain.InterfacesInternal;
using Domain.InterfacesInternal.InterfacesServices;
using Entities.Entities;

namespace Domain.ServicesInternal
{
    public class ServiceCliente : IServiceCliente
    {
        public readonly IClienteInfrastructure _IclienteInfrastructure;
        public readonly IPhoneClienteInfrastructure _IphoneClienteInfrastructure;

        public ServiceCliente(IClienteInfrastructure iclienteInfrastructure, IPhoneClienteInfrastructure iphoneClienteInfrastructure)
        {
            _IclienteInfrastructure = iclienteInfrastructure;
            _IphoneClienteInfrastructure = iphoneClienteInfrastructure;
        }

        public async Task<bool> AtualizarClienteFull(Cliente cliente)
        {
            await _IclienteInfrastructure.Update(cliente);
            return true;
        }

        public async Task<bool> AtualizarEmailCliente(string emailAntigo, string emailNovo)
        {
            var cliente = await _IclienteInfrastructure.GetByEmailAsync(emailAntigo);
            cliente.Email = emailNovo;
            await _IclienteInfrastructure.Update(cliente);
            return true;
        }

        public async Task<bool> AtualizarTelefoneCliente(int clientId, int telefoneIdAntigo, PhoneCliente phoneClienteNovo)
        {
            var cliente = await _IclienteInfrastructure.GetEntityById(clientId);
            var phone = await _IphoneClienteInfrastructure.GetEntityById(telefoneIdAntigo);
            var check = phone.ClienteId == cliente.ClienteId;

            if (check)
            {
                phone.DDD = phoneClienteNovo.DDD;
                phone.PhoneNumber = phoneClienteNovo.PhoneNumber;
                phone.IsCellPhone = phoneClienteNovo.IsCellPhone;

                await _IphoneClienteInfrastructure.Update(phone);
            }
            return check;
        }

        public bool CadastrarCliente(Cliente cliente)
        {
            _ = _IclienteInfrastructure.Add(cliente);
            return true;
        }

        public async Task<bool> DeleteClienteByEmail(string email)
        {
            var cliente = await _IclienteInfrastructure.GetByEmailAsync(email);

            if (cliente is not null)
            {
                await _IclienteInfrastructure.Delete(cliente);
                return true;
            }

            return false;
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