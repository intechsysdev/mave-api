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
    public class CarroController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public CarroController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoDTO>> Get()
        {
            var resultado = await _moduloEcommerceAppService.ConsultarProductosCarro();

            return resultado;
        }

        [HttpPut()]
        public async Task Put(ProductoDTO producto)
        {
            await _moduloEcommerceAppService.ActualizarProductoCarro(producto).ConfigureAwait(false);
        }
    }
}