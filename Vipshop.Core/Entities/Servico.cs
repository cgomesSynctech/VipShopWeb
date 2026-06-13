using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipshopWeb.Core.Entities
{
    public class Servico : Item
    {
        public decimal QuantidadeMaoObra { get; set; }
        public int TempoOcupacao { get; set; } // Em minutos ou horas, conforme sua regra

        public Servico()
        {
            Tipo = TipoItem.Servico;
        }
    }
}
