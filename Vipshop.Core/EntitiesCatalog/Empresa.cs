using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.EntitiesCatalog
{
    [Table("empresas")]
    public class Empresa
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(14)]
        [Column("cnpj_cpf")]
        public string CnpjCpf { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        [Column("razao_social")]
        public string RazaoSocial { get; set; } = string.Empty;

        [MaxLength(150)]
        [Column("nome_fantasia")]
        public string? NomeFantasia { get; set; }

        [Column("data_contratacao")]
        public DateTime DataContratacao { get; set; } = DateTime.UtcNow;

        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}
