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
    public class PromocionController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public PromocionController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<PromocionDTO>> Get()
        {
            var resultado = await _moduloEcommerceAppService.ConsultarPromociones();

            return resultado;
        }
    }
}