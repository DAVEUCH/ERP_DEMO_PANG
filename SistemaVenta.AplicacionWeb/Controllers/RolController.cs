using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRolService _rolServicio;

        public RolController(IMapper mapper, IRolService rolServicio)
        {
            _mapper = mapper;
            _rolServicio = rolServicio;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {

            List<VMRol> vmRolLista = _mapper.Map<List<VMRol>>(await _rolServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmRolLista });

        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMRol modelo)
        {
            GenericResponse<VMRol> gResponse = new GenericResponse<VMRol>();

            try
            {
                Rol rol_creado = await _rolServicio.Crear(_mapper.Map<Rol>(modelo));
                modelo = _mapper.Map<VMRol>(rol_creado);

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

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMRol modelo)
        {
            GenericResponse<VMRol> gResponse = new GenericResponse<VMRol>();

            try
            {
                Rol rol_editado = await _rolServicio.Editar(_mapper.Map<Rol>(modelo));
                modelo = _mapper.Map<VMRol>(rol_editado);

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

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idRol)
        {

            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _rolServicio.Eliminar(idRol);

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
