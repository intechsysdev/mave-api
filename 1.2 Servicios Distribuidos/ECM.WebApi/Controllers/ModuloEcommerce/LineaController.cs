using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloEcommerce
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineaController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public LineaController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<LineaDTO>> Get()
        {
            var resultado = await _moduloEcommerceAppService.ConsultarLineasCliente();

            return resultado;
        }
    }
}