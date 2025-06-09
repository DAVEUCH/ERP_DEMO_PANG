using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SistemaVenta.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro);
        Task<TEntity> Crear(TEntity entidad);
        Task<bool> Editar(TEntity entidad);
        Task<bool> Eliminar(TEntity entidad);
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null);

    }
}
//contexto:
//En esta sección, el presentador explica la creación de una vista de perfil de usuario en una aplicación ASP.NET Core y 
//soluciona un error que aparece al intentar acceder a ella. El proceso comienza copiando y modificando un método existente
// en el controlador Home para crear un nuevo método llamado "perfil". A continuación, se añade una nueva vista y se edita 
//su contenido HTML para incluir la estructura necesaria. El presentador también actualiza el archivo de diseño para corregir 
//la URL de la vista de perfil. Tras confirmar que la vista de perfil funciona, el enfoque se centra en el desarrollo de la 
//capa de datos de la aplicación mediante la introducción de una interfaz genérica para las operaciones del repositorio, que
//incluye métodos para operaciones de datos comunes como crear, leer, actualizar y eliminar entidades.