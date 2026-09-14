using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusuarioController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public MusuarioController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{cust}/{succli}")]
        public async Task<EcmMusuarioDTO> Get(string cust, string succli)
        {
            var resultado = await _gestionSeg.ConsultarUsuario(cust, succli).ConfigureAwait(false);

            return resultado;
        }
    }
}