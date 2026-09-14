using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.ModuloApp;
using ECM.Aplicacion.Servicios.Interfaz.ModuloApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloApp
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoEmpresaController : ControllerBase
    {
        readonly IModuloAppAppService _moduloAppAppService;

        public ContactoEmpresaController(IModuloAppAppService moduloAppAppService)
        {
            _moduloAppAppService = moduloAppAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactoEmpresaDTO>> Get()
        {
            var resultado = await _moduloAppAppService.ConsultarContactosEmpresa();

            return resultado;
        }
    }
}