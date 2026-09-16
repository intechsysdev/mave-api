using System.Collections.Generic;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Gupshup;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.Messages
{
    /// <summary>
    /// Administracion del catalogo local de plantillas de WhatsApp. Permite dar de alta
    /// las plantillas aprobadas en Gupshup sin recompilar ni tocar el appsettings.
    /// </summary>
    [Route("api/whatsapp/templates")]
    [ApiController]
    [Authorize]
    public class WhatsappTemplatesController : ControllerBase
    {
        private readonly IWhatsappTemplateData _templateData;

        private readonly IGupshupService _gupshupService;

        private readonly IContextAccessor _contextAccessor;

        public WhatsappTemplatesController(
            IWhatsappTemplateData templateData,
            IGupshupService gupshupService,
            IContextAccessor contextAccessor)
        {
            _templateData = templateData;
            _gupshupService = gupshupService;
            _contextAccessor = contextAccessor;
        }

        /// <summary>Lista todas las plantillas configuradas.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<WhatsappTemplateDTO>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_templateData.GetAll());
        }

        /// <summary>Lista las plantillas configuradas para una empresa.</summary>
        [HttpGet("{tenant}")]
        [ProducesResponseType(typeof(List<WhatsappTemplateDTO>), 200)]
        public IActionResult GetByTenant(string tenant)
        {
            return Ok(_templateData.GetAll(ResolverEmpresa(tenant)));
        }

        /// <summary>Consulta una plantilla por empresa y nombre logico.</summary>
        [HttpGet("{tenant}/{name}")]
        [ProducesResponseType(typeof(WhatsappTemplateDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(string tenant, string name)
        {
            var plantilla = _templateData.Get(ResolverEmpresa(tenant), name);

            return plantilla == null ? NotFound() : Ok(plantilla);
        }

        /// <summary>Crea o reemplaza una plantilla. La llave es empresa + nombre logico.</summary>
        [HttpPost]
        [HttpPut]
        [ProducesResponseType(typeof(WhatsappTemplateDTO), 200)]
        [ProducesResponseType(400)]
        public IActionResult Save([FromBody] WhatsappTemplateDTO template)
        {
            if (template == null)
            {
                return BadRequest("El cuerpo de la solicitud es obligatorio.");
            }

            template.Tenant = ResolverEmpresa(template.Tenant);

            if (string.IsNullOrWhiteSpace(template.Tenant) || string.IsNullOrWhiteSpace(template.Name))
            {
                return BadRequest("La plantilla debe indicar Tenant y Name.");
            }

            return Ok(_templateData.Save(template));
        }

        /// <summary>Elimina una plantilla del catalogo local.</summary>
        [HttpDelete("{tenant}/{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(string tenant, string name)
        {
            return _templateData.Delete(ResolverEmpresa(tenant), name) ? NoContent() : NotFound();
        }

        /// <summary>Vuelve a leer el archivo de plantillas desde disco.</summary>
        [HttpPost("reload")]
        public IActionResult Reload()
        {
            _templateData.Reload();

            return Ok(new { recargado = true });
        }

        /// <summary>
        /// Consulta en Gupshup las plantillas realmente aprobadas para el App de la
        /// empresa. Sirve para contrastar el catalogo local con el remoto.
        /// </summary>
        [HttpGet("remote/{tenant}")]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 200)]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 400)]
        public async Task<IActionResult> GetRemote(string tenant)
        {
            var result = await _gupshupService.GetRemoteTemplates(ResolverEmpresa(tenant)).ConfigureAwait(false);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        private string ResolverEmpresa(string tenant)
        {
            return string.IsNullOrWhiteSpace(tenant) ? _contextAccessor.CompanyId : tenant;
        }
    }
}
