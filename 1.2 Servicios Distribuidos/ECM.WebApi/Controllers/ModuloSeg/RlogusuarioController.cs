using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class RlogusuarioController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public RlogusuarioController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EcmRlogusuarioDTO item)
        {
            await _gestionSeg.RegistrarLogUsuario(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }       
    }
}