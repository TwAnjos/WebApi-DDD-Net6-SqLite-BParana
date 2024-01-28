using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public virtual List<PhoneCliente> PhoneCliente { get; set; }
    }
}
