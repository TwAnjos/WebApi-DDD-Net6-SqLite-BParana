using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class PhoneCliente
    {
        public int PhoneClienteId { get; set; }
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public bool IsCellPhone { get; set; }

        [ForeignKey("Cliente")]
        [Column("ClienteId", Order = 1)]
        public int ClienteId { get; set; }
    }
}