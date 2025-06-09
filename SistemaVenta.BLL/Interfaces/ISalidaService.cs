using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ISalidaService
    {
        Task<List<Producto>> ObtenerProductos(string busqueda);

        Task<Salida> Registrar(Salida entidad);

        Task<List<Salida>> Historial(string numeroVenta, string fechaInicio, string fechaFin);

        Task<Salida> Detalle(string numeroVenta);
        Task<List<DetalleSalida>> Reporte(string fechaInicio, string fechaFin);
    }
}
