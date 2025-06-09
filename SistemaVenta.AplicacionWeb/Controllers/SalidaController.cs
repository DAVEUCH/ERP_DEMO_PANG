using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SistemaVenta.Entity.Models;


namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class SalidaController : Controller
    {
        private readonly ITipoDocumentoOCService _tipoDocumentoOCService;
        private readonly ISalidaService _salidaServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public SalidaController(ITipoDocumentoOCService tipoDocumentoOCService,
        ISalidaService salidaServicio,
        IMapper mapper,
        IConverter converter
    )
        {
            _tipoDocumentoOCService = tipoDocumentoOCService;
            _salidaServicio = salidaServicio;
            _mapper = mapper;
            _converter = converter;
        }
        public IActionResult NuevaSalida()
        {
            return View();
        }

        public IActionResult HistorialSalida()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoSalida()
        {

            List<VMTipoDocumentoOC> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumentoOC>>(await _tipoDocumentoOCService.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper.Map<List<VMProducto>>(await _salidaServicio.ObtenerProductos(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarSalida([FromBody] VMSalida modelo)
        {

            GenericResponse<VMSalida> gResponse = new GenericResponse<VMSalida>();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);

                Salida salida_creada = await _salidaServicio.Registrar(_mapper.Map<Salida>(modelo));
                modelo = _mapper.Map<VMSalida>(salida_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex)
            {

                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroSalida, string fechaInicio, string fechaFin)
        {

            List<VMSalida> vmHistorialSalida = _mapper.Map<List<VMSalida>>(await _salidaServicio.Historial(numeroSalida, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialSalida);
        }

        public IActionResult MostrarPDFSalida(string numeroSalida)
        {

            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/PlantillaSalida/PDFSalida?numeroSalida={numeroSalida}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings(){
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");

        }

    }
}
