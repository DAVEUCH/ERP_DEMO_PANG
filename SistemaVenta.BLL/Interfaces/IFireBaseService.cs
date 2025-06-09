using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IFireBaseService
    {

        Task<string> SubirStorage(Stream StreamArchivo,string CarpetaDestino);
        Task<bool> EliminarStorage(string CarpetaDestino);

    }
}
