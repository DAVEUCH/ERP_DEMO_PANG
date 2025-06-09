using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity.Models
{
    public partial class NumeroCorrelativo2
    {
        public int IdNumeroCorrelativo2 { get; set; }
        public int? UltimoNumero { get; set; }
        public int? CantidadDigitos { get; set; }
        public string? Gestion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
