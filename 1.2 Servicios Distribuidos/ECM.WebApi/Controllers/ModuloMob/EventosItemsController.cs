using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosItemsController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public EventosItemsController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{type}/{id}")]
        public async Task<IEnumerable<MobEventosItemsDTO>> Get(string cmpy, string type, string id)
        {
            var resultado = await _gestionMob.ConsultarEventosItemsHoy(cmpy, type, id).ConfigureAwait(false);
                        
            return resultado;
        }

        [HttpGet("{cmpy}/{type}")]
        public async Task<IEnumerable<MobEventosItemsDTO>> Get(string cmpy, string type)
        {
            var resultado = await _gestionMob.ConsultarEventosItemsHoy(cmpy, type).ConfigureAwait(false);

            return resultado;
        }

    }
}