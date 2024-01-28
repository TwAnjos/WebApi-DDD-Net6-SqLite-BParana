using Domain.InterfacesInternal.InterfacesServices;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.RepositoriesInternal
{
    public class RepositoryPhoneCliente: RepositoryGenerics<PhoneCliente>, IPhoneClienteInfrastructure
    {
        private readonly DbContextOptions<ContextBase> _OptionBuilder;

        public RepositoryPhoneCliente()
        {
            _OptionBuilder = new DbContextOptions<ContextBase>();
        }
    }
}
