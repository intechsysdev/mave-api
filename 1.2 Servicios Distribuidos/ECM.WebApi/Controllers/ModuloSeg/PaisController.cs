using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public PaisController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{id}")]
        public async Task<EcmPaisDTO> Get(int id)
        {
            var resultado = await _gestionSeg.ConsultarPais(id).ConfigureAwait(false);

            return resultado;
        }
    }
}