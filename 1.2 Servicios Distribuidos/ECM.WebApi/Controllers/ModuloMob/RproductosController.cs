using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RproductosController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RproductosController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{id}")]
        public async Task<MobRproductosDTO> Get(string cmpy, string id)
        {
            var resultado = await _gestionMob.ConsultarProducto(cmpy, id).ConfigureAwait(false);
                        
            return resultado;
        }

    }
}