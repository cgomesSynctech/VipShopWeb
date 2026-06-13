using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("fornecedores")]
    public class Fornecedor
    {
        [Key]
        [Column("favorecido_id")]
        public Guid FavorecidoId { get; set; }

        [ForeignKey(nameof(FavorecidoId))]
        public virtual Favorecido Favorecido { get; set; } = null!;

        [MaxLength(20)]
        [Column("inscricao_estadual")]
        public string? InscricaoEstadual { get; set; }

        [Column("prazo_entrega_estimado_dias")]
        public int PrazoEntregaEstimadoDias { get; set; }
    }
}
