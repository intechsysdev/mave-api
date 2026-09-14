using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.ModuloEcommerce
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParametroController : ControllerBase
    {
        readonly IModuloEcommerceAppService _moduloEcommerceAppService;

        public ParametroController(IModuloEcommerceAppService moduloEcommerceAppService)
        {
            _moduloEcommerceAppService = moduloEcommerceAppService;
        }
        [HttpGet]
        public async Task<ParametersDTO> Get()
        {
            return await _moduloEcommerceAppService.ConsultarParametros();
        }
    }
}