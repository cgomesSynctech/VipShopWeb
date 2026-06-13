using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("favorecidos")]
    public class Favorecido
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("tipo_favorecido_id")]
        public int TipoFavorecidoId { get; set; }

        [ForeignKey(nameof(TipoFavorecidoId))]
        public virtual TipoFavorecido TipoFavorecido { get; set; } = null!;

        [Required, MaxLength(150)]
        [Column("nome_razao_social")]
        public string NomeRazaoSocial { get; set; } = string.Empty;

        [MaxLength(150)]
        [Column("nome_fantasia_apelido")]
        public string? NomeFantasiaApelido { get; set; }

        [Required, MaxLength(14)]
        [Column("documento")]
        public string Documento { get; set; } = string.Empty;

        [Required, MaxLength(2)]
        [Column("tipo_pessoa")]
        public string TipoPessoa { get; set; } = "PJ"; // PF ou PJ

        [Column("data_cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Propriedades de Navegação Virtuais (Herança TPT)
        public virtual Cliente? Cliente { get; set; }
        public virtual Fornecedor? Fornecedor { get; set; }
        public virtual Funcionario? Funcionario { get; set; }
    }
}
