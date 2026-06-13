using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    [Table("tipos_favorecidos")]
    public class TipoFavorecido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Controlado manualmente (1, 2 ou 3)
        [Column("id")]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;
    }
}
