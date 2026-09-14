using System.Threading.Tasks;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloCliente
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TerminosController : ControllerBase
    {

        readonly IModuloAppAppService _moduloAppAppService;

        public TerminosController(IModuloAppAppService moduloAppAppService)
        {
            _moduloAppAppService = moduloAppAppService;
        }

        [HttpPost()]
        public async Task Post()
        {
            await _moduloAppAppService.AceptarTerminosYCondiciones().ConfigureAwait(false);
        }
    }
}