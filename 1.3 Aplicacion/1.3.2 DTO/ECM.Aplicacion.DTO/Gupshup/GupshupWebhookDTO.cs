using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECM.Aplicacion.DTO.Gupshup
{
    /// <summary>
    /// Evento que Gupshup envia al webhook configurado en el App.
    /// El contenido de "payload" cambia segun el valor de "type", por eso los nombres
    /// respetan los del json original.
    /// </summary>
    public class GupshupWebhookEvent
    {
        /// <summary>Nombre del App de Gupshup que origina el evento. Permite resolver la empresa.</summary>
        public string app { get; set; }

        /// <summary>Marca de tiempo del evento en milisegundos.</summary>
        public long timestamp { get; set; }

        /// <summary>Version del formato del webhook.</summary>
        public int version { get; set; }

        /// <summary>Tipo de evento: message, message-event, user-event, template-event, billing-event.</summary>
        public string type { get; set; }

        public GupshupWebhookPayload payload { get; set; }
    }

    /// <summary>
    /// Cuerpo del evento. Agrupa los campos de los eventos de estado (message-event) y
    /// de los mensajes entrantes (message).
    /// </summary>
    public class GupshupWebhookPayload
    {
        /// <summary>Identificador del mensaje. Coincide con el messageId devuelto al enviar.</summary>
        public string id { get; set; }

        /// <summary>Identificador interno de Gupshup.</summary>
        public string gsId { get; set; }

        /// <summary>
        /// En message-event: enqueued, sent, delivered, read, failed, deleted.
        /// En message: text, image, video, file, audio, location, contact, button_reply, list_reply.
        /// </summary>
        public string type { get; set; }

        /// <summary>Numero destino en los eventos de estado.</summary>
        public string destination { get; set; }

        /// <summary>Numero origen en los mensajes entrantes.</summary>
        public string source { get; set; }

        /// <summary>Nombre de la plantilla o del contacto, segun el evento.</summary>
        public string name { get; set; }

        /// <summary>Codigo de error reportado por WhatsApp cuando el evento es failed.</summary>
        public string code { get; set; }

        /// <summary>Motivo del fallo cuando aplica.</summary>
        public string reason { get; set; }

        /// <summary>Informacion del remitente en los mensajes entrantes.</summary>
        public GupshupWebhookSender sender { get; set; }

        /// <summary>
        /// Contenido especifico del evento. Se conserva como json crudo por la variedad
        /// de formatos que puede tomar.
        /// </summary>
        public JsonElement? payload { get; set; }

        /// <summary>Texto del mensaje entrante cuando el evento es de tipo text.</summary>
        [JsonIgnore]
        public string Text
        {
            get
            {
                if (payload.HasValue
                    && payload.Value.ValueKind == JsonValueKind.Object
                    && payload.Value.TryGetProperty("text", out var text)
                    && text.ValueKind == JsonValueKind.String)
                {
                    return text.GetString();
                }

                return null;
            }
        }
    }

    /// <summary>
    /// Datos del remitente de un mensaje entrante.
    /// </summary>
    public class GupshupWebhookSender
    {
        public string phone { get; set; }

        public string name { get; set; }

        public string country_code { get; set; }

        public string dial_code { get; set; }
    }

    /// <summary>
    /// Resultado del procesamiento de un evento del webhook.
    /// </summary>
    public class GupshupWebhookResultDTO
    {
        /// <summary>Indica si el evento se pudo interpretar y clasificar.</summary>
        public bool Handled { get; set; }

        /// <summary>Empresa a la que corresponde el evento.</summary>
        public string Tenant { get; set; }

        /// <summary>Tipo de evento recibido.</summary>
        public string EventType { get; set; }

        /// <summary>Identificador del mensaje al que se refiere el evento.</summary>
        public string MessageId { get; set; }

        /// <summary>Numero del contacto involucrado.</summary>
        public string Phone { get; set; }

        /// <summary>Estado interno equivalente al evento, segun StateMapping.</summary>
        public string MappedState { get; set; }

        /// <summary>Texto del mensaje cuando se trata de un mensaje entrante.</summary>
        public string Text { get; set; }

        /// <summary>Explicacion de lo que se hizo con el evento.</summary>
        public string Detail { get; set; }
    }
}
