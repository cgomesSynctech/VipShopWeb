using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipshopWeb.Core.Entities
{
    [Table("grupos_produtos")]
    public class Grupo
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(20)]
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;
    }
}

