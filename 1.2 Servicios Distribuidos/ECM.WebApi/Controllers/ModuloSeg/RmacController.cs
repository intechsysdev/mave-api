using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class RmacController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public RmacController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }


        //[HttpGet()]
        //public async Task Get(EcmRmacDTO item)
        //{
        //    await _gestionSeg.RegistrarMac(item).ConfigureAwait(false);
        //}

        [HttpPost()]
        public async Task<IActionResult> Post(EcmRmacDTO item)
        {
            await _gestionSeg.RegistrarMac(item).ConfigureAwait(false);

            return new OkObjectResult(new { valid = true });
        }

        //[HttpPut()]
        //public async Task Put(EcmRmacDTO item)
        //{
        //    await _gestionSeg.RegistrarMac(item).ConfigureAwait(false);
        //}
    }
}