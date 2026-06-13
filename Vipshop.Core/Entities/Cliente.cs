using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("favorecido_id")]
        public Guid FavorecidoId { get; set; }

        [ForeignKey(nameof(FavorecidoId))]
        public virtual Favorecido Favorecido { get; set; } = null!;

        [Column("limite_credito")]
        public decimal LimiteCredito { get; set; }

        [Column("observacao")]
        public string? Observacao { get; set; }
    }
}
