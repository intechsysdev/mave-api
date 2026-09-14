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
    public class ProductoController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public ProductoController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoDTO>> Get()
        {
            var resultado = await _moduloEcommerceAppService.ConsultarProductosCliente();
          
            return resultado;
        }
    }
}