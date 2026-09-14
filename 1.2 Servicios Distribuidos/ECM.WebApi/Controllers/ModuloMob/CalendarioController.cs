using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public CalendarioController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}")]
        public async Task<IEnumerable<MobCalendarioDTO>> Get(string cmpy)
        {
            var resultado = await _gestionMob.ConsultarCalendarioHoy(cmpy).ConfigureAwait(false);
                        
            return resultado;
        }

    }
}