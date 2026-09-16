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
    /// Envio de mensajes de WhatsApp a traves de Gupshup.
    ///
    /// Cuando la solicitud no indica empresa se usa la del usuario autenticado, que es
    /// el caso habitual desde el ecommerce.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WhatsappController : ControllerBase
    {
        private readonly IGupshupService _gupshupService;

        private readonly IContextAccessor _contextAccessor;

        public WhatsappController(IGupshupService gupshupService, IContextAccessor contextAccessor)
        {
            _gupshupService = gupshupService;
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Envia una plantilla aprobada. La plantilla se indica por TemplateId o por
        /// TemplateName, y los parametros por posicion (Params) o por nombre (Values).
        /// </summary>
        [HttpPost("send/template")]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 200)]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 400)]
        public async Task<IActionResult> SendTemplate([FromBody] GupshupTemplateRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(GupshupApiResultDTO.Fail("El cuerpo de la solicitud es obligatorio."));
            }

            request.Tenant = ResolverEmpresa(request.Tenant);

            var result = await _gupshupService.SendTemplate(request).ConfigureAwait(false);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Envia la misma plantilla a varios destinos y devuelve el resultado individual
        /// de cada envio.
        /// </summary>
        [HttpPost("send/template/bulk")]
        [ProducesResponseType(typeof(List<GupshupApiResultDTO>), 200)]
        public async Task<IActionResult> SendTemplateBulk([FromBody] List<GupshupTemplateRequestDTO> requests)
        {
            if (requests == null || requests.Count == 0)
            {
                return BadRequest(GupshupApiResultDTO.Fail("Debe enviar al menos un registro."));
            }

            var results = new List<GupshupApiResultDTO>();

            foreach (var request in requests)
            {
                request.Tenant = ResolverEmpresa(request.Tenant);

                results.Add(await _gupshupService.SendTemplate(request).ConfigureAwait(false));
            }

            return Ok(results);
        }

        /// <summary>
        /// Envia un mensaje de texto libre. Solo aplica dentro de la ventana de sesion de
        /// 24 horas posterior al ultimo mensaje del usuario.
        /// </summary>
        [HttpPost("send/text")]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 200)]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 400)]
        public async Task<IActionResult> SendText([FromBody] GupshupTextRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(GupshupApiResultDTO.Fail("El cuerpo de la solicitud es obligatorio."));
            }

            request.Tenant = ResolverEmpresa(request.Tenant);

            var result = await _gupshupService.SendText(request).ConfigureAwait(false);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>Registra el opt-in de un numero en el App de Gupshup.</summary>
        [HttpPost("optin")]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 200)]
        [ProducesResponseType(typeof(GupshupApiResultDTO), 400)]
        public async Task<IActionResult> OptIn([FromBody] GupshupOptInRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(GupshupApiResultDTO.Fail("El cuerpo de la solicitud es obligatorio."));
            }

            request.Tenant = ResolverEmpresa(request.Tenant);

            var result = await _gupshupService.OptIn(request).ConfigureAwait(false);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Consulta la configuracion efectiva de Gupshup para una empresa. La ApiKey se
        /// devuelve enmascarada: solo sirve para verificar que quedo configurada.
        /// </summary>
        [HttpGet("config")]
        [HttpGet("config/{tenant}")]
        public IActionResult GetConfig(string tenant)
        {
            var app = _gupshupService.GetAppSettings(ResolverEmpresa(tenant));

            return Ok(new
            {
                app.Tenant,
                app.Enabled,
                app.AppName,
                app.AppId,
                app.SourceNumber,
                app.CallbackUrl,
                ApiKeyConfigurada = !string.IsNullOrWhiteSpace(app.ApiKey),
                ApiKey = Enmascarar(app.ApiKey)
            });
        }

        private string ResolverEmpresa(string tenant)
        {
            return string.IsNullOrWhiteSpace(tenant) ? _contextAccessor.CompanyId : tenant;
        }

        private static string Enmascarar(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Length <= 4
                ? new string('*', value.Length)
                : new string('*', value.Length - 4) + value.Substring(value.Length - 4);
        }
    }
}
