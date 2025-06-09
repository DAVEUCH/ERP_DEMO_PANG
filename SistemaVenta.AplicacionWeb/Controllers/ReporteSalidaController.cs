using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class ReporteSalidaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISalidaService _salidaServicio;

        public ReporteSalidaController(IMapper mapper, ISalidaService salidaServicio)
        {
            _mapper = mapper;
            _salidaServicio = salidaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ReporteSalida(string fechaInicio, string fechaFin)
        {
            List<VMReporteSalida> vmLista = _mapper.Map<List<VMReporteSalida>>(await _salidaServicio.Reporte(fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }
    }
}
