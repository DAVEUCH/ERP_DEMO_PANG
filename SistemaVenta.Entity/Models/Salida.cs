using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.Entity.Models
{
    public partial class Salida
    {
        public Salida()
        {
            DetalleSalida = new HashSet<DetalleSalida>();
        }

        public int IdSalida { get; set; }
        public string? NumeroSalida { get; set; }
        public int? IdTipoDocumentoOc { get; set; }
        public int? IdUsuario { get; set; }
        public string? DocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ImpuestoTotal { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual TipoDocumentoOC? IdTipoDocumentoOCNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleSalida> DetalleSalida { get; set; }

    }
}
