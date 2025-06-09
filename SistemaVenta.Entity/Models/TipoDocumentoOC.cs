using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.Entity.Models
{
    public partial class TipoDocumentoOC
    {
        public TipoDocumentoOC()
        {
            Salida = new HashSet<Salida>();
        }

        public int IdTipoDocumentoOC { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Salida> Salida { get; set; }
    }
}
