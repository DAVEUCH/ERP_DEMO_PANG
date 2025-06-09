using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.BLL.Implementacion
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _repositorio;

        public RolService(IGenericRepository<Rol> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Rol>> Lista()
        {

            IQueryable<Rol> query = await _repositorio.Consultar();

            return query.ToList();

        }

        public async Task<Rol> Crear(Rol entidad)
        {
            try
            {
                Rol rol_creado = await _repositorio.Crear(entidad);
                if (rol_creado.IdRol == 0)
                    throw new TaskCanceledException("No se pudo crear el rol");

                return rol_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Rol> Editar(Rol entidad)
        {
            try
            {
                Rol rol_encontrado = await _repositorio.Obtener(c => c.IdRol == entidad.IdRol);
                rol_encontrado.Descripcion = entidad.Descripcion;
                rol_encontrado.EsActivo = entidad.EsActivo;

                bool respuesta = await _repositorio.Editar(rol_encontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo modificar el rol");

                return rol_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idRol)
        {
            try
            {
                Rol rol_encontrado = await _repositorio.Obtener(c => c.IdRol == idRol);

                if (rol_encontrado == null)
                    throw new TaskCanceledException("La rol no existe");

                bool respuesta = await _repositorio.Eliminar(rol_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
