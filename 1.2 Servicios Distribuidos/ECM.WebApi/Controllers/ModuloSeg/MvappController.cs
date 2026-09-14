using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MvappController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MvappController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{appId}/{tappId}")]
        public async Task<EcmMvappDTO> Get(int appId, int tappId)
        {
            var resultado = await _gestionSeg.ConsultarVersionAplicacion(appId, tappId).ConfigureAwait(false);

            return resultado;
        }
    }
}