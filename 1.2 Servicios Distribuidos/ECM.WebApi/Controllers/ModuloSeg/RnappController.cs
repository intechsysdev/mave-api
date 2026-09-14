using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class RnappController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public RnappController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EcmRnappDTO item)
        {
            await _gestionSeg.RegistrarAccesoApp(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }       
    }
}