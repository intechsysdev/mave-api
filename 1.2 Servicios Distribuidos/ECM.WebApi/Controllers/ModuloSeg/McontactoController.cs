using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloSeg;
using ECM.Aplicacion.Servicios.Interfaz.ModuloSeg;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloSeg
{
    [Route("api/[controller]")]
    [ApiController]
    public class McontactoController : ControllerBase
    {
        readonly IGestionSegAppService _gestionSeg;

        public McontactoController(IGestionSegAppService gestionSeg)
        {
            _gestionSeg = gestionSeg;
        }

        [HttpGet("{emprId}/{id}")]
        public async Task<EcmMcontactoDTO> Get(string emprId, int id)
        {
            var resultado = await _gestionSeg.ConsultarContacto(emprId, id).ConfigureAwait(false);

            return resultado;
        }

        [HttpGet("{emprId}")]
        public async Task<IEnumerable<EcmMcontactoDTO>> Get(string emprId)
        {
            var resultado = await _gestionSeg.ConsultarContactosEmpresa(emprId).ConfigureAwait(false);

            return resultado;
        }
    }
}