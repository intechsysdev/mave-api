using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteraController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public CarteraController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{ccust}/{succli}")]
        public async Task<IEnumerable<MobCarteraDTO>> Get(string cmpy, string ccust, string succli)
        {
            var resultado = await _gestionMob.ConsultarCarteraCliente(cmpy, ccust, succli).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpGet("{cmpy}/{ccust}/{succli}/{mesesCartera}")]
        public async Task<IEnumerable<MobCarteraDTO>> Get(string cmpy, string ccust, string succli, int mesesCartera)
        {
            var resultado = await _gestionMob.ConsultarCarteraCliente(cmpy, ccust, succli, mesesCartera).ConfigureAwait(false);

            return resultado;
        }

    }
}