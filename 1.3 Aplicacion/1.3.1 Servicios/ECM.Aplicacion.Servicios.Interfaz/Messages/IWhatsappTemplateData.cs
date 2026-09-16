using System.Collections.Generic;
using ECM.Aplicacion.DTO.Gupshup;

namespace ECM.Aplicacion.Servicios.Interfaz.Messages
{
    /// <summary>
    /// Catalogo de plantillas de WhatsApp configuradas localmente.
    ///
    /// Se mantiene en un archivo json y no en la base porque las plantillas cambian al
    /// ritmo de las aprobaciones de Meta, no al de los despliegues.
    /// </summary>
    public interface IWhatsappTemplateData
    {
        /// <summary>Devuelve todas las plantillas configuradas.</summary>
        IEnumerable<WhatsappTemplateDTO> GetAll();

        /// <summary>Devuelve las plantillas configuradas para una empresa.</summary>
        IEnumerable<WhatsappTemplateDTO> GetAll(string tenant);

        /// <summary>Consulta una plantilla por empresa y nombre logico. Devuelve null si no existe.</summary>
        WhatsappTemplateDTO Get(string tenant, string name);

        /// <summary>Crea o reemplaza una plantilla. La llave es empresa + nombre logico.</summary>
        WhatsappTemplateDTO Save(WhatsappTemplateDTO template);

        /// <summary>Elimina una plantilla. Devuelve false si no existia.</summary>
        bool Delete(string tenant, string name);

        /// <summary>Vuelve a leer el archivo de configuracion.</summary>
        void Reload();
    }
}
