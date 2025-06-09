using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlantillaSalidaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioServicio;
        private readonly ISalidaService _salidaServicio;


        public PlantillaSalidaController(IMapper mapper,
    INegocioService negocioServicio,
    ISalidaService salidaServicio)
        {
            _mapper = mapper;
            _negocioServicio = negocioServicio;
            _salidaServicio = salidaServicio;
        }

        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            return View();
        }

        public async Task<IActionResult> PDFSalida(string numeroSalida)
        {

            VMSalida vmSalida = _mapper.Map<VMSalida>(await _salidaServicio.Detalle(numeroSalida));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFSalida modelo = new VMPDFSalida();

            modelo.negocio = vmNegocio;
            modelo.salida = vmSalida;

            return View(modelo);
        }
        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
    }
}
