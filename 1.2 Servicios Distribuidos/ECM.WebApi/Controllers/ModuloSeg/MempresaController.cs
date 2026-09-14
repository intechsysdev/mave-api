using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MempresaController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MempresaController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{id}")]
        public async Task<EcmMempresaDTO> Get(string id)
        {
            var resultado = await _gestionSeg.ConsultarEmpresa(id).ConfigureAwait(false);

            return resultado;
        }
    }
}