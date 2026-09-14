using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RprodCustController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RprodCustController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{succli}")]
        public async Task<IEnumerable<MobRprodCustDTO>> Get(string cmpy, string cust, string succli)
        {
            var resultado = await _gestionMob.ConsultarProductosCliente(cmpy, cust, succli).ConfigureAwait(false);
                        
            return resultado;
        }

    }
}