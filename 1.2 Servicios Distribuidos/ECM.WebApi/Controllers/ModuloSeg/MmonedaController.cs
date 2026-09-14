using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MmonedaController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MmonedaController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{id}")]
        public async Task<EcmMmonedaDTO> Get(int id)
        {
            var resultado = await _gestionSeg.ConsultarMoneda(id).ConfigureAwait(false);

            return resultado;
        }
    }
}