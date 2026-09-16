using System;
using ECM.Aplicacion.DTO.Gupshup;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECM.WebApi.Controllers.Messages
{
    /// <summary>
    /// Webhook que recibe los eventos de Gupshup: cambios de estado de los mensajes de
    /// WhatsApp y mensajes entrantes de los usuarios.
    ///
    /// Es anonimo porque lo invoca Gupshup, no el ecommerce; la autenticacion se hace
    /// con el token configurado en WebhookToken.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GupshupController : ControllerBase
    {
        private readonly IGupshupService _gupshupService;

        private readonly GupshupSettings _settings;

        private readonly ILogger<GupshupController> _logger;

        public GupshupController(
            IGupshupService gupshupService,
            IOptions<GupshupSettings> settings,
            ILogger<GupshupController> logger)
        {
            _gupshupService = gupshupService;
            _settings = settings.Value;
            _logger = logger;
        }

        /// <summary>
        /// Verificacion de la url del webhook. Gupshup consulta este endpoint al
        /// registrar el callback en el App.
        /// </summary>
        [HttpGet("webhook")]
        [HttpGet("webhook/{tenant}")]
        public IActionResult Verify(string tenant)
        {
            return Ok(new { status = "ok", tenant });
        }

        /// <summary>
        /// Recibe los eventos de Gupshup. La empresa se resuelve por el nombre del App
        /// que viene en el evento, usando el mapeo del appsettings.
        /// </summary>
        [HttpPost("webhook")]
        public IActionResult Webhook([FromBody] GupshupWebhookEvent webhookEvent)
        {
            return Process(webhookEvent, null);
        }

        /// <summary>
        /// Recibe los eventos forzando la empresa por ruta. Se usa cuando cada empresa
        /// registra su propia url de callback en Gupshup.
        /// </summary>
        [HttpPost("webhook/{tenant}")]
        public IActionResult Webhook(string tenant, [FromBody] GupshupWebhookEvent webhookEvent)
        {
            return Process(webhookEvent, tenant);
        }

        private IActionResult Process(GupshupWebhookEvent webhookEvent, string tenant)
        {
            if (!_gupshupService.IsValidWebhookToken(LeerToken()))
            {
                _logger.LogWarning("Webhook de Gupshup rechazado por token invalido.");

                return Unauthorized();
            }

            try
            {
                var result = _gupshupService.ProcessWebhook(webhookEvent, tenant);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Gupshup reintenta y puede deshabilitar el callback ante respuestas de
                // error, por eso el webhook siempre responde 200 y el fallo queda en el log.
                _logger.LogError(ex, "Error procesando el webhook de Gupshup.");

                return Ok(new GupshupWebhookResultDTO
                {
                    Handled = false,
                    EventType = webhookEvent?.type,
                    Detail = ex.Message
                });
            }
        }

        private string LeerToken()
        {
            var cabecera = string.IsNullOrWhiteSpace(_settings.WebhookTokenHeader)
                ? "X-Gupshup-Token"
                : _settings.WebhookTokenHeader;

            if (Request.Headers.TryGetValue(cabecera, out var valorCabecera))
            {
                return valorCabecera.ToString();
            }

            return Request.Query.TryGetValue("token", out var valorQuery) ? valorQuery.ToString() : null;
        }
    }
}
