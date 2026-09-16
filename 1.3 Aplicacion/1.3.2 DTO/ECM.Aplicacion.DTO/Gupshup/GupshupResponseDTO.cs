using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Gupshup
{
    /// <summary>
    /// Respuesta cruda del API de Gupshup al enviar un mensaje. Los nombres respetan
    /// los del json original.
    /// </summary>
    public class GupshupSendResponse
    {
        public string status { get; set; }

        public string messageId { get; set; }

        public string message { get; set; }
    }

    /// <summary>
    /// Resultado normalizado de una llamada al API de Gupshup.
    /// </summary>
    public class GupshupApiResultDTO
    {
        /// <summary>Indica si Gupshup acepto la solicitud.</summary>
        public bool Success { get; set; }

        /// <summary>Codigo http devuelto por Gupshup.</summary>
        public int HttpStatusCode { get; set; }

        /// <summary>Estado reportado por Gupshup. Ej: submitted.</summary>
        public string Status { get; set; }

        /// <summary>Identificador del mensaje generado por Gupshup.</summary>
        public string MessageId { get; set; }

        /// <summary>Descripcion del error cuando la solicitud no fue aceptada.</summary>
        public string ErrorMessage { get; set; }

        /// <summary>Cuerpo original de la respuesta. Util para diagnostico.</summary>
        public string RawResponse { get; set; }

        /// <summary>Plantilla efectivamente utilizada en el envio.</summary>
        public string TemplateId { get; set; }

        /// <summary>Destino al que se envio el mensaje.</summary>
        public string Destination { get; set; }

        /// <summary>Dato de correlacion enviado por el consumidor del API.</summary>
        public string CallbackData { get; set; }

        public static GupshupApiResultDTO Fail(string error, int httpStatusCode = 0, string raw = null)
        {
            return new GupshupApiResultDTO
            {
                Success = false,
                ErrorMessage = error,
                HttpStatusCode = httpStatusCode,
                RawResponse = raw
            };
        }
    }

    /// <summary>
    /// Plantilla tal como la reporta Gupshup en la consulta del App.
    /// </summary>
    public class GupshupRemoteTemplate
    {
        public string id { get; set; }

        public string elementName { get; set; }

        public string languageCode { get; set; }

        public string category { get; set; }

        public string status { get; set; }

        public string templateType { get; set; }

        public string data { get; set; }
    }

    /// <summary>
    /// Respuesta de la consulta de plantillas registradas en el App de Gupshup.
    /// </summary>
    public class GupshupRemoteTemplateList
    {
        public string status { get; set; }

        public List<GupshupRemoteTemplate> templates { get; set; }
    }
}
