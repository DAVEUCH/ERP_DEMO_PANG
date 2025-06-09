using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.DAL.Implementacion
{
    public class SalidaRepository : GenericRepository<Salida>,ISalidaRepository
    {
        private readonly DBVENTAContext _dbContext;

        public SalidaRepository(DBVENTAContext dbContex) : base(dbContex)
        {
            _dbContext = dbContex;
        }

        public async Task<Salida> Registrar(Salida entidad)
        {
            Salida ventaGenerada = new Salida();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    foreach (DetalleSalida dv in entidad.DetalleSalida)
                    {

                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();


                    NumeroCorrelativo2 correlativo = _dbContext.NumeroCorrelativos2.Where(n => n.Gestion == "salida").First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativos2.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroSalida = ceros + correlativo.UltimoNumero.ToString();
                    numeroSalida = numeroSalida.Substring(numeroSalida.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroSalida = numeroSalida;

                    await _dbContext.Salida.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    ventaGenerada = entidad;

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }


            }

            return ventaGenerada;
        }

        public async Task<List<DetalleSalida>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleSalida> listaResumen = await _dbContext.DetalleSalida
                .Include(v => v.IdSalidaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdSalidaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoOCNavigation)
                .Where(dv => dv.IdSalidaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                    dv.IdSalidaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listaResumen;
        }


    }
}
