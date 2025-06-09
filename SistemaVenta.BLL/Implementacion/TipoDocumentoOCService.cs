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
    public class TipoDocumentoOCService : ITipoDocumentoOCService
    {
        private readonly IGenericRepository<TipoDocumentoOC> _repositorio;
        
        public TipoDocumentoOCService(IGenericRepository<TipoDocumentoOC> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TipoDocumentoOC>> Lista()
        {
            IQueryable<TipoDocumentoOC> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
