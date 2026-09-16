using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Gupshup
{
    /// <summary>
    /// Configuracion local de una plantilla de WhatsApp. Permite invocar las plantillas
    /// por un nombre logico sin tener que conocer el id que asigna Gupshup ni recompilar
    /// cuando se aprueba una plantilla nueva.
    /// </summary>
    public class WhatsappTemplateDTO
    {
        /// <summary>Nombre logico con el que se invoca la plantilla.</summary>
        public string Name { get; set; }

        /// <summary>Empresa propietaria de la plantilla.</summary>
        public string Tenant { get; set; }

        /// <summary>Id (uuid) de la plantilla aprobada en Gupshup.</summary>
        public string TemplateId { get; set; }

        /// <summary>Nombre del elemento tal como quedo registrado en Gupshup.</summary>
        public string ElementName { get; set; }

        /// <summary>Codigo de idioma. Ej: es, es_CO, en_US.</summary>
        public string Language { get; set; }

        /// <summary>Categoria declarada en Gupshup: MARKETING, UTILITY o AUTHENTICATION.</summary>
        public string Category { get; set; }

        /// <summary>Cuerpo de la plantilla con sus marcadores. Sirve de referencia y vista previa.</summary>
        public string Body { get; set; }

        /// <summary>
        /// Nombres de los parametros en el orden que espera la plantilla. Permite enviar
        /// Values por nombre en lugar de Params por posicion.
        /// </summary>
        public List<string> ParamNames { get; set; }

        /// <summary>Tipo de cabecera: none, text, image, video o document.</summary>
        public string HeaderType { get; set; }

        /// <summary>Url por defecto de la media de cabecera cuando aplica.</summary>
        public string HeaderUrl { get; set; }

        /// <summary>Nombre por defecto del archivo de cabecera cuando la cabecera es un documento.</summary>
        public string HeaderFilename { get; set; }

        /// <summary>Indica si la plantilla esta habilitada para su uso.</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>Descripcion funcional de la plantilla.</summary>
        public string Description { get; set; }

        /// <summary>Fecha de la ultima actualizacion de la configuracion.</summary>
        public string UpdatedAt { get; set; }
    }
}
