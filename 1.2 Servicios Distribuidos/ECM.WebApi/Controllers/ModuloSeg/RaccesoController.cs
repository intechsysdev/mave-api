using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaccesoController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public RaccesoController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EcmRaccesoDTO item)
        {
            await _gestionSeg.RegistrarAcceso(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }       
    }
}