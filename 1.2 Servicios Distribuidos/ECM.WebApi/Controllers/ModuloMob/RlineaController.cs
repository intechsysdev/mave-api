using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RlineaController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RlineaController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{succli}")]
        public IEnumerable<MobRlineaDTO> Get(string cmpy, string cust, string succli)
        {
            var resultado = _gestionMob.ConsultarLineasCliente(cmpy, cust, succli);
                        
            return resultado;
        }

    }
}