using ECM.Aplicacion.DTO.Cliente;
using ECM.Aplicacion.Servicios.Interfaz.ModuloCliente;
using Itdear.ServiciosDistribuidos.WebApi.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.WebApi.Controllers.ModuloCliente
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarteraController : ControllerBase
    {

        readonly IModuloClienteAppService _moduloCliente;

        public CarteraController(IModuloClienteAppService moduloCliente)
        {
            _moduloCliente = moduloCliente;
        }

        [HttpGet]
        public async Task<IEnumerable<CarteraDTO>> Get()
        {
            return await _moduloCliente.ConsultarCarteraCliente(null).ConfigureAwait(false);
        }

        [HttpGet("{mesesCartera}")]
        public async Task<IEnumerable<CarteraDTO>> Get(int? mesesCartera)
        {
            return await _moduloCliente.ConsultarCarteraCliente(mesesCartera).ConfigureAwait(false);
        }
    }
}