using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RordHeadCustController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RordHeadCustController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{ship}/{hist}")]
        public async Task<IEnumerable<MobRordHeadCustDTO>> Get(string cmpy, string cust, string ship, int hist)
        {
            var resultado = await _gestionMob.ConsultarEncabezadosOrdenes(cmpy, cust, ship, hist).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(MobRordHeadCustDTO item)
        {
            await _gestionMob.CrearEncabezadoOrden(item);

            return new OkObjectResult(new { valid = true });
        }


        [HttpPut()]
        public async Task<IActionResult> Put(MobRordHeadCustDTO item)
        {
            await _gestionMob.ActualizarEncabezadoOrden(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(MobRordHeadCustDTO item)
        {
            await _gestionMob.EliminarEncabezadoOrden(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }

    }
}