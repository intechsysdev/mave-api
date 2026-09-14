using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class RpasswordController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public RpasswordController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EcmRpasswordDTO item)
        {
            await _gestionSeg.RegistrarPassword(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }       
    }
}