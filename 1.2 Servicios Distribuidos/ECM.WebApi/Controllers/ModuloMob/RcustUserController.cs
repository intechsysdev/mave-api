using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RcustUserController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RcustUserController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{succli}")]
        public async Task<MobRcustUserDTO> Get(string cmpy, string cust, string succli)
        {
            var resultado = await _gestionMob.ConsultarClienteUsuario(cmpy, cust, succli).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpPut("{cmpy}/{cust}/{succli}")]
        public async Task<ActionResult<MobRcustUserDTO>> Put(string cmpy, string cust, string succli, [FromBody]MobRcustUserDTO item)
        {
            if (item == null || cmpy != item.MrcuCmpy || cust != item.MrcuCust || succli != item.MrcuSuccli)
            {
                return BadRequest();
            }

            var resultado = await _gestionMob.ActualizarClienteUsuario(item).ConfigureAwait(false);

            return resultado;
        }

    }
}