using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.WebApi.Controllers.ModuloEcommerce
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventoController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public EventoController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventoDTO>> Get()
        {
            var resultado = await _moduloEcommerceAppService.ConsultarEventos();

            return resultado;
        }
    }
}