using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;

namespace ECM.WebApi.Controllers.ModuloMob
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSublineasController : ControllerBase
    {

        readonly IGestionModAppService _gestionMob;

        public RSublineasController(IGestionModAppService gestionMob)
        {
            _gestionMob = gestionMob;
        }

        [HttpGet("{cmpy}/{cust}/{succli}")]
        public IEnumerable<MobRsubLineasDTO> Get(string cmpy, string cust, string succli)
        {
            var resultado = _gestionMob.ConsultarSubLineasCliente(cmpy, cust, succli);
                        
            return resultado;
        }

    }
}