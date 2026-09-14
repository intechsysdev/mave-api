using ECM.Aplicacion.DTO.ModuloApp;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.WebApi.Controllers.ModuloApp
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NovedadAppController : ControllerBase
    {
        readonly IModuloAppAppService _moduloAppAppService;

        public NovedadAppController(IModuloAppAppService moduloAppAppService)
        {
            _moduloAppAppService = moduloAppAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<NovedadAppDTO>> Get()
        {
            var resultado = await _moduloAppAppService.ConsultarNovedades();

            return resultado;
        }

        [HttpPost()]
        public async Task Post([FromBody]int idNovedad)
        {
            await _moduloAppAppService.AceptarNovedades(idNovedad).ConfigureAwait(false);
        }
    }
}