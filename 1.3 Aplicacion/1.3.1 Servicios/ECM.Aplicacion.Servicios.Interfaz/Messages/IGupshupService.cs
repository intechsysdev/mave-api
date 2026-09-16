using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Gupshup;

namespace ECM.Aplicacion.Servicios.Interfaz.Messages
{
    /// <summary>
    /// Cliente del API de WhatsApp de Gupshup.
    /// </summary>
    public interface IGupshupService
    {
        /// <summary>Envia una plantilla (HSM) aprobada.</summary>
        Task<GupshupApiResultDTO> SendTemplate(GupshupTemplateRequestDTO request);

        /// <summary>Envia un mensaje de texto dentro de la ventana de sesion de 24 horas.</summary>
        Task<GupshupApiResultDTO> SendText(GupshupTextRequestDTO request);

        /// <summary>Registra el opt-in de un numero en el App de Gupshup.</summary>
        Task<GupshupApiResultDTO> OptIn(GupshupOptInRequestDTO request);

        /// <summary>Consulta las plantillas registradas en el App de Gupshup.</summary>
        Task<GupshupApiResultDTO> GetRemoteTemplates(string tenant);

        /// <summary>Interpreta un evento del webhook y lo clasifica.</summary>
        GupshupWebhookResultDTO ProcessWebhook(GupshupWebhookEvent webhookEvent, string tenant = null);

        /// <summary>Valida el token del webhook cuando hay uno configurado.</summary>
        bool IsValidWebhookToken(string token);

        /// <summary>Devuelve la configuracion efectiva de una empresa.</summary>
        GupshupAppSettings GetAppSettings(string tenant);
    }
}
