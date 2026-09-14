using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Cliente;
using ECM.Aplicacion.Servicios.Interfaz.ModuloCliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloCliente
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        readonly IModuloClienteAppService _moduloClienteAppService;

        public MenuController(IModuloClienteAppService moduloClienteAppService)
        {
            _moduloClienteAppService = moduloClienteAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<MenuDTO>> Get()
        {
            var resultado = await _moduloClienteAppService.ConsultarMenuUsuario();

            return resultado;
        }
    }
}