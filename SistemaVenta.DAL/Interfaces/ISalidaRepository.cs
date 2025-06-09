using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.DAL.Interfaces
{
    public interface ISalidaRepository : IGenericRepository<Salida>
    {
        Task<Salida> Registrar(Salida entidad);
        Task<List<DetalleSalida>> Reporte(DateTime FechaInicio, DateTime FechaFin);
    }
}
