using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionesController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public PromocionesController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}")]
        public async Task<IEnumerable<MobPromocionesDTO>> Get(string cmpy)
        {
            var resultado = await _gestionMob.ConsultarPromocionesHoy(cmpy).ConfigureAwait(false);
                        
            return resultado;
        }

    }
}