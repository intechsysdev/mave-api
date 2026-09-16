using System;
using System.Collections.Generic;
using System.Linq;

namespace ECM.Aplicacion.DTO.Gupshup
{
    /// <summary>
    /// Configuracion general de la integracion con Gupshup (seccion "Gupshup" del appsettings.json).
    ///
    /// El "tenant" corresponde a la empresa del ecommerce, el mismo codigo que viaja en
    /// el claim CompanyId y que se guarda en ecm_mempresa.mempr_cmpy. Los valores por
    /// empresa (Apps) sobrescriben los valores globales cuando vienen informados.
    /// </summary>
    public class GupshupSettings
    {
        /// <summary>Habilita o deshabilita el envio real hacia Gupshup.</summary>
        public bool Enabled { get; set; }

        /// <summary>Url base del API de mensajeria. Ej: https://api.gupshup.io/wa/api/v1/ </summary>
        public string BaseUrl { get; set; }

        /// <summary>Url base del API de partner/app. Ej: https://api.gupshup.io/wa/app/ </summary>
        public string AppUrl { get; set; }

        /// <summary>ApiKey global (cabecera "apikey"). Se puede sobrescribir por empresa.</summary>
        public string ApiKey { get; set; }

        /// <summary>Nombre del App de Gupshup (parametro src.name).</summary>
        public string AppName { get; set; }

        /// <summary>Identificador del App de Gupshup (opt-in y consulta de plantillas).</summary>
        public string AppId { get; set; }

        /// <summary>Numero de WhatsApp origen registrado en Gupshup (parametro source).</summary>
        public string SourceNumber { get; set; }

        /// <summary>Url publica del webhook registrada en Gupshup. Solo informativa.</summary>
        public string CallbackUrl { get; set; }

        /// <summary>Token opcional para validar el webhook. Si esta vacio no se valida.</summary>
        public string WebhookToken { get; set; }

        /// <summary>Nombre de la cabecera donde se espera el token del webhook.</summary>
        public string WebhookTokenHeader { get; set; }

        /// <summary>Ruta del archivo json con la configuracion de plantillas.</summary>
        public string TemplatesFile { get; set; }

        /// <summary>Timeout en segundos de las llamadas al API.</summary>
        public int TimeoutSeconds { get; set; }

        /// <summary>Empresa que se asume cuando el webhook no permite resolverla por el nombre del App.</summary>
        public string DefaultTenant { get; set; }

        /// <summary>Configuracion particular por empresa.</summary>
        public List<GupshupAppSettings> Apps { get; set; }

        /// <summary>
        /// Traduccion entre el tipo de evento de Gupshup (enqueued, sent, delivered,
        /// read, failed) y el estado interno con el que se quiera registrar el mensaje.
        /// </summary>
        public Dictionary<string, string> StateMapping { get; set; }

        /// <summary>
        /// Resuelve la configuracion efectiva de una empresa combinando sus valores
        /// particulares con los globales.
        /// </summary>
        public GupshupAppSettings ResolveApp(string tenant)
        {
            GupshupAppSettings app = null;

            if (Apps != null && !string.IsNullOrWhiteSpace(tenant))
            {
                app = Apps.FirstOrDefault(x => string.Equals(x.Tenant, tenant, StringComparison.OrdinalIgnoreCase));
            }

            return new GupshupAppSettings
            {
                Tenant = app?.Tenant ?? tenant,

                // La bandera global manda: si la integracion esta apagada, ninguna
                // empresa puede habilitarla por su cuenta.
                Enabled = app == null ? Enabled : (app.Enabled && Enabled),

                ApiKey = Coalesce(app?.ApiKey, ApiKey),
                AppName = Coalesce(app?.AppName, AppName),
                AppId = Coalesce(app?.AppId, AppId),
                SourceNumber = Coalesce(app?.SourceNumber, SourceNumber),
                CallbackUrl = Coalesce(app?.CallbackUrl, CallbackUrl)
            };
        }

        /// <summary>
        /// Resuelve la empresa a partir del nombre del App que origina el webhook.
        /// </summary>
        public string ResolveTenantByAppName(string appName)
        {
            if (Apps != null && !string.IsNullOrWhiteSpace(appName))
            {
                var app = Apps.FirstOrDefault(
                    x => string.Equals(x.AppName, appName, StringComparison.OrdinalIgnoreCase));

                if (app != null)
                {
                    return app.Tenant;
                }
            }

            return DefaultTenant;
        }

        /// <summary>
        /// Traduce el tipo de evento de Gupshup al estado interno.
        /// Devuelve null cuando el evento no esta mapeado.
        /// </summary>
        public string MapState(string eventType)
        {
            if (StateMapping == null || string.IsNullOrWhiteSpace(eventType))
            {
                return null;
            }

            var item = StateMapping.FirstOrDefault(
                x => string.Equals(x.Key, eventType, StringComparison.OrdinalIgnoreCase));

            return string.IsNullOrWhiteSpace(item.Value) ? null : item.Value;
        }

        private static string Coalesce(string value, string fallback)
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : value;
        }
    }

    /// <summary>
    /// Configuracion del App de Gupshup asociado a una empresa.
    /// </summary>
    public class GupshupAppSettings
    {
        /// <summary>Codigo de la empresa (mempr_cmpy).</summary>
        public string Tenant { get; set; }

        public bool Enabled { get; set; } = true;

        public string ApiKey { get; set; }

        public string AppName { get; set; }

        public string AppId { get; set; }

        public string SourceNumber { get; set; }

        public string CallbackUrl { get; set; }
    }
}
