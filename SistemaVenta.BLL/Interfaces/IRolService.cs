using SistemaVenta.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> Lista();
        Task<Rol> Crear(Rol entidad);
        Task<Rol> Editar(Rol entidad);
        Task<bool> Eliminar(int idRol);
    }
}
