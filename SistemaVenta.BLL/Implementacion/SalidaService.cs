using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Implementacion
{
    public class SalidaService : ISalidaService
    {
        private readonly IGenericRepository<Producto> _repositorioProducto;
        private readonly ISalidaRepository _repositorioSalida; 

        public SalidaService(IGenericRepository<Producto> repositorioProducto,
                             ISalidaRepository repositorioSalida)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioSalida = repositorioSalida;
        }


        public async Task<List<Producto>> ObtenerProductos(string busqueda)
        {
            IQueryable<Producto> query = await _repositorioProducto.Consultar(p =>
                p.EsActivo == true &&
                p.Stock > 0 &&
                string.Concat(p.CodigoBarra, p.Marca, p.Descripcion).Contains(busqueda)
                );

            return query.Include(c => c.IdCategoriaNavigation).ToList();
        }

        public async Task<Salida> Registrar(Salida entidad)
        {
            try
            {
                return await _repositorioSalida.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Salida>> Historial(string numeroSalida, string fechaInicio, string fechaFin)
        {
            IQueryable<Salida> query = await _repositorioSalida.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;


            if (fechaInicio != "" && fechaFin != "")
            {

                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_fin.Date
                )
                    .Include(tdv => tdv.IdTipoDocumentoOCNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dv => dv.DetalleSalida)
                    .ToList();
            }
            else
            {
                return query.Where(v => v.NumeroSalida == numeroSalida
                   )
                       .Include(tdv => tdv.IdTipoDocumentoOCNavigation)
                       .Include(u => u.IdUsuarioNavigation)
                       .Include(dv => dv.DetalleSalida)
                       .ToList();
            }

        }

        public async Task<Salida> Detalle(string numeroVenta)
        {
            IQueryable<Salida> query = await _repositorioSalida.Consultar(v => v.NumeroSalida == numeroVenta);

            return query
                        .Include(tdv => tdv.IdTipoDocumentoOCNavigation)
                        .Include(u => u.IdUsuarioNavigation)
                        .Include(dv => dv.DetalleSalida)
                        .First();
        }

        public async Task<List<DetalleSalida>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

            List<DetalleSalida> lista = await _repositorioSalida.Reporte(fech_inicio, fech_fin);

            return lista;
        }

    }
}
