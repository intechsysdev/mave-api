using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RordLineCustController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RordLineCustController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{num}/{cust}/{ship}")]
        public async Task<IEnumerable<MobRordLineCustDTO>> Get(int num, string cust, string ship)
        {
            var resultado = await _gestionMob.ConsultarDetalleOrden(num, cust, ship).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(MobRordLineCustDTO item)
        {
            await _gestionMob.CrearDetalleOrden(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateItems(IEnumerable<MobRordLineCustDTO> items)
        {
            await _gestionMob.CrearDetallesOrden(items).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }


        [HttpPut()]
        public async Task<IActionResult> Put(MobRordLineCustDTO item)
        {
            await _gestionMob.ActualizarDetalleOrden(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateItems(IEnumerable<MobRordLineCustDTO> items)
        {
            await _gestionMob.ActualizarDetallesOrden(items).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }


        [HttpDelete()]
        public async Task<IActionResult> Delete(MobRordLineCustDTO item)
        {
            await _gestionMob.EliminarDetalleOrden(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }

    }
}