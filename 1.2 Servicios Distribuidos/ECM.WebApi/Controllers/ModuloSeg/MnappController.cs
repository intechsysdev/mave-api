using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MnappController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MnappController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{nappId}/{vappId}/{tappId}")]
        public async Task<EcmMnappDTO> Get(int nappId, int vappId, int tappId)
        {
            var resultado = await _gestionSeg.ConsultarNovedadAplicacion(nappId, vappId, tappId).ConfigureAwait(false);

            return resultado;
        }


        [HttpGet("{vappId}/{tappId}")]
        public async Task<IEnumerable<EcmMnappDTO>> Get(int vappId, int tappId)
        {
            var resultado = await _gestionSeg.ConsultarNovedadesAplicacion(vappId, tappId).ConfigureAwait(false);

            return resultado;
        }
    }
}