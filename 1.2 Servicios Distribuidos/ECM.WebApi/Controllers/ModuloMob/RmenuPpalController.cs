using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RmenuPpalController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RmenuPpalController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{codper}")]
        public async Task<MobRperfilesDTO> Get(string cmpy, string codper)
        {
            var resultado = await _gestionMob.ConsultarPerfilUsuario(cmpy, codper).ConfigureAwait(false);
                        
            return resultado;
        }

    }
}