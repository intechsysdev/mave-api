using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RcustConsecController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RcustConsecController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{succli}/{tipdoc}")]
        public async Task<MobRcustConsecDTO> Get(string cmpy, string cust, string succli, string tipdoc)
        {
            var resultado = await _gestionMob.ConsultarConsecutivoCliente(cmpy, cust, succli, tipdoc).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpPost("{cmpy}/{cust}/{succli}/{tipdoc}")]
        public async Task<ActionResult<MobRcustConsecDTO>> Post(string cmpy, string cust, string succli, string tipdoc, [FromBody]MobRcustConsecDTO item)
        {
            if (item == null || cmpy != item.MdcCmpy || cust != item.MdcCust || succli != item.MdcSuccli || tipdoc != item.MdcTipdoc)
            {
                return BadRequest();
            }

            var resultado = await _gestionMob.CrearConsecutivoCliente(item).ConfigureAwait(false);

            return resultado;
        }

    }
}