using Domain.InterfacesInternal;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.RepositoriesInternal
{
    public class RepositoryCliente : RepositoryGenerics<Cliente>, IClienteInfrastructure
    {
        private readonly DbContextOptions<ContextBase> _OptionBuilder;

        public RepositoryCliente()
        {
            _OptionBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Cliente>> GetAllClientes(Expression<Func<Cliente, bool>> expression)
        {
            using (var db = new ContextBase(_OptionBuilder))
            {
                return await db.Cliente.Where(expression).Include(x => x.PhoneCliente).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Cliente>> GetAllClientesByDDDNumero(string numero)
        {
            using (var db = new ContextBase(_OptionBuilder))
            {
                var pn = await db.PhoneCliente.Where(x => x.DDD.Contains(numero) || x.PhoneNumber.Contains(numero)).Select(x => x.ClienteId).ToListAsync();
                var cliente = await db.Cliente.Where(x => pn.Contains(x.ClienteId)).ToListAsync();
                return cliente;
            }
        }

        public async Task<List<Cliente>> GetAllClientesById(List<PhoneCliente> phones)
        {
            using (var db = new ContextBase(_OptionBuilder))
            {
                var r = await db.Cliente.Where(x => x.ClienteId.Equals(phones.Select(y => y))).ToListAsync();
                return r;
            }
        }

        public async Task<Cliente> GetByEmailAsync(string emailAntigo)
        {
            using (var db = new ContextBase(_OptionBuilder))
            {
                var r = await db.Cliente.Where(x => x.Email == emailAntigo).FirstOrDefaultAsync();
                return r;
            }
        }
    }
}