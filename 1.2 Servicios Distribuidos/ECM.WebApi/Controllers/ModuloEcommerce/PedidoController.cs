using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloEcommerce
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public PedidoController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }

        [HttpGet("{fechaIni}/{fechaFin}")]
        public async Task<IEnumerable<PedidoDTO>> Get(DateTime fechaIni, DateTime fechaFin)
        {
            return await _moduloEcommerceAppService.ConsultarPedidos(fechaIni, fechaFin);
        }

        [HttpGet("{idPedido}")]
        public async Task<PedidoDTO> Get(int idPedido)
        {
            return await _moduloEcommerceAppService.ConsultarPedido(idPedido);
        }

        [HttpGet("[action]/{token}")]
        [AllowAnonymous]
        public async Task<PedidoDTO> GetByToken(Guid token)
        {
            return await _moduloEcommerceAppService.ConsultarPedido(token);
        }

        [HttpPost()]
        public async Task<int> Post([FromBody]PedidoDTO item)
        {
            return await _moduloEcommerceAppService.CrearPedido(item).ConfigureAwait(false);
        }

        [HttpDelete("{idPedido}")]
        public async Task<ActionResult> Delete(int idPedido)
        {
            await _moduloEcommerceAppService.EliminarPedido(idPedido).ConfigureAwait(false);

            return NoContent();
        }        
    }
}