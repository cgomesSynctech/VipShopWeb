using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    public class Produto : Item
    {
        public string UnidadeVenda { get; set; } = string.Empty; // Ex: UN, KG, PC
        [Required]
        [Column("grupo_id")]
        public Guid GrupoId { get; set; }

        [ForeignKey(nameof(GrupoId))]
        public virtual Grupo Grupo { get; set; } = null!;
        public string Referencia { get; set; } = string.Empty;
        [MaxLength(50)]
        [Column("codigo_barras")]
        public string? CodigoBarras { get; set; }

        [Column("quantidade_estoque")]
        public decimal QuantidadeEstoque { get; set; }

        public Produto()
        {
            Tipo = TipoItem.Produto;
        }
    }
}
