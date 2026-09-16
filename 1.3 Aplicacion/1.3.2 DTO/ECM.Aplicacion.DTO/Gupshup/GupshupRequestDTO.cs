using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Gupshup
{
    /// <summary>
    /// Envio de una plantilla (HSM) de WhatsApp. La plantilla se puede indicar por
    /// TemplateId (el uuid que entrega Gupshup) o por TemplateName (el nombre logico
    /// configurado en WhatsappTemplates.json).
    /// </summary>
    public class GupshupTemplateRequestDTO
    {
        /// <summary>Empresa a la que pertenece el envio. Resuelve App, ApiKey y numero origen.</summary>
        public string Tenant { get; set; }

        /// <summary>Numero destino en formato internacional sin signos. Ej: 573160181995</summary>
        public string Destination { get; set; }

        /// <summary>Id de la plantilla aprobada en Gupshup. Opcional si se envia TemplateName.</summary>
        public string TemplateId { get; set; }

        /// <summary>Nombre logico de la plantilla configurada. Opcional si se envia TemplateId.</summary>
        public string TemplateName { get; set; }

        /// <summary>Parametros posicionales de la plantilla (los marcadores 1, 2, 3...).</summary>
        public List<string> Params { get; set; }

        /// <summary>
        /// Parametros por nombre. Se ordenan usando ParamNames de la plantilla
        /// configurada. Se ignora cuando se envia Params.
        /// </summary>
        public Dictionary<string, string> Values { get; set; }

        /// <summary>Media opcional para la cabecera de la plantilla.</summary>
        public GupshupMediaDTO Media { get; set; }

        /// <summary>Textos de los botones tipo quick reply con postback.</summary>
        public List<string> PostbackTexts { get; set; }

        /// <summary>Numero origen. Si viene vacio se toma el configurado para la empresa.</summary>
        public string Source { get; set; }

        /// <summary>Nombre del App de Gupshup. Si viene vacio se toma el configurado para la empresa.</summary>
        public string SrcName { get; set; }

        /// <summary>Dato libre de correlacion que se devuelve en la respuesta del envio.</summary>
        public string CallbackData { get; set; }
    }

    /// <summary>
    /// Envio de un mensaje de sesion (ventana de 24 horas) por WhatsApp.
    /// </summary>
    public class GupshupTextRequestDTO
    {
        public string Tenant { get; set; }

        public string Destination { get; set; }

        public string Text { get; set; }

        /// <summary>Indica si WhatsApp debe generar la vista previa de los enlaces del texto.</summary>
        public bool PreviewUrl { get; set; }

        public string Source { get; set; }

        public string SrcName { get; set; }
    }

    /// <summary>
    /// Media adjunta a la cabecera de una plantilla.
    /// </summary>
    public class GupshupMediaDTO
    {
        /// <summary>Tipo de media: image, video, document o text.</summary>
        public string Type { get; set; }

        /// <summary>Url publica del archivo.</summary>
        public string Url { get; set; }

        /// <summary>Nombre del archivo. Aplica para documentos.</summary>
        public string Filename { get; set; }

        /// <summary>Texto que acompana la media.</summary>
        public string Caption { get; set; }
    }

    /// <summary>
    /// Solicitud de opt-in de un numero en el App de Gupshup.
    /// </summary>
    public class GupshupOptInRequestDTO
    {
        public string Tenant { get; set; }

        public string Phone { get; set; }
    }
}
