using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MtappController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MtappController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{id}")]
        public async Task<EcmMtappDTO> Get(int id)
        {
            var resultado = await _gestionSeg.ConsultarTipoAplicacion(id).ConfigureAwait(false);

            return resultado;
        }
    }
}