using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(50)]
        [Column("login")]
        public string Login { get; set; } = string.Empty;

        [Required, MaxLength(255)] // Armazenar Hash MD5 / SHA256 / BCrypt
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}
