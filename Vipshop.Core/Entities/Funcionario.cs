using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("funcionarios")]
    public class Funcionario
    {
        [Key]
        [Column("favorecido_id")]
        public Guid FavorecidoId { get; set; }

        [ForeignKey(nameof(FavorecidoId))]
        public virtual Favorecido Favorecido { get; set; } = null!;

        [MaxLength(20)]
        [Column("ctps")]
        public string? Ctps { get; set; }

        [Column("salario")]
        public decimal Salario { get; set; }

        [Column("data_admissao")]
        public DateTime DataAdmissao { get; set; }

        [Required, MaxLength(100)]
        [Column("cargo")]
        public string Cargo { get; set; } = string.Empty;
    }
}
