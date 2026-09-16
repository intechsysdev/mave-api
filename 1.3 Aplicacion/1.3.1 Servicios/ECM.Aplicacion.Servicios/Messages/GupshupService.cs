using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Gupshup;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECM.Aplicacion.Servicios.Messages
{
    /// <summary>
    /// Cliente del API de WhatsApp de Gupshup.
    ///
    /// Toda la configuracion (ApiKey, App, numero origen y mapeo de estados) proviene de
    /// la seccion "Gupshup" del appsettings. La ApiKey viaja en cada peticion y no en el
    /// HttpClient porque cambia segun la empresa.
    /// </summary>
    public class GupshupService : IGupshupService
    {
        private const string UrlBasePorDefecto = "https://api.gupshup.io/wa/api/v1/";

        private const string UrlAppPorDefecto = "https://api.gupshup.io/wa/app/";

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly IHttpClientFactory _clientFactory;

        private readonly IWhatsappTemplateData _templateData;

        private readonly ILogger _logger;

        private readonly GupshupSettings _settings;

        public GupshupService(
            IHttpClientFactory clientFactory,
            IWhatsappTemplateData templateData,
            ILogger<GupshupService> logger,
            IOptions<GupshupSettings> settings)
        {
            _clientFactory = clientFactory;
            _templateData = templateData;
            _logger = logger;
            _settings = settings.Value;
        }

        public GupshupAppSettings GetAppSettings(string tenant)
        {
            return _settings.ResolveApp(tenant);
        }

        /// <summary>
        /// Envia una plantilla aprobada. La plantilla se resuelve por TemplateId o, en su
        /// defecto, por el nombre logico configurado en WhatsappTemplates.json.
        /// </summary>
        public async Task<GupshupApiResultDTO> SendTemplate(GupshupTemplateRequestDTO request)
        {
            if (request == null)
            {
                return GupshupApiResultDTO.Fail("El cuerpo de la solicitud es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Destination))
            {
                return GupshupApiResultDTO.Fail("El campo Destination es obligatorio.");
            }

            var app = _settings.ResolveApp(request.Tenant);

            var validacion = Validar(app);

            if (validacion != null)
            {
                return validacion;
            }

            var plantilla = string.IsNullOrWhiteSpace(request.TemplateName)
                ? null
                : _templateData.Get(request.Tenant, request.TemplateName);

            if (plantilla != null && !plantilla.Enabled)
            {
                return GupshupApiResultDTO.Fail(
                    $"La plantilla '{request.TemplateName}' esta deshabilitada.");
            }

            var templateId = string.IsNullOrWhiteSpace(request.TemplateId)
                ? plantilla?.TemplateId
                : request.TemplateId;

            if (string.IsNullOrWhiteSpace(templateId))
            {
                return GupshupApiResultDTO.Fail(string.IsNullOrWhiteSpace(request.TemplateName)
                    ? "Debe indicar TemplateId o TemplateName."
                    : $"No existe configuracion para la plantilla '{request.TemplateName}' de la empresa '{request.Tenant}'.");
            }

            var parametros = ResolverParametros(request, plantilla);

            var template = new Dictionary<string, object>
            {
                { "id", templateId },
                { "params", parametros }
            };

            var form = new Dictionary<string, string>
            {
                { "channel", "whatsapp" },
                { "source", PrimeroNoVacio(request.Source, app.SourceNumber) },
                { "destination", SoloDigitos(request.Destination) },
                { "src.name", PrimeroNoVacio(request.SrcName, app.AppName) },
                { "template", JsonSerializer.Serialize(template) }
            };

            var media = ResolverMedia(request, plantilla);

            if (media != null)
            {
                form.Add("message", JsonSerializer.Serialize(media));
            }

            if (request.PostbackTexts != null && request.PostbackTexts.Count > 0)
            {
                var postbacks = request.PostbackTexts
                    .Select((text, index) => new Dictionary<string, object>
                    {
                        { "index", index },
                        { "text", text }
                    })
                    .ToList();

                form.Add("postbackTexts", JsonSerializer.Serialize(postbacks));
            }

            var result = await PostForm(app, "template/msg", form).ConfigureAwait(false);

            result.TemplateId = templateId;
            result.Destination = request.Destination;
            result.CallbackData = request.CallbackData;

            RegistrarEnvio(request.Tenant, "template", request.Destination, result);

            return result;
        }

        /// <summary>
        /// Envia un mensaje de texto. Solo aplica dentro de la ventana de sesion de 24
        /// horas posterior al ultimo mensaje del usuario.
        /// </summary>
        public async Task<GupshupApiResultDTO> SendText(GupshupTextRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Destination))
            {
                return GupshupApiResultDTO.Fail("El campo Destination es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return GupshupApiResultDTO.Fail("El campo Text es obligatorio.");
            }

            var app = _settings.ResolveApp(request.Tenant);

            var validacion = Validar(app);

            if (validacion != null)
            {
                return validacion;
            }

            var message = new Dictionary<string, object>
            {
                { "type", "text" },
                { "text", request.Text },
                { "previewUrl", request.PreviewUrl }
            };

            var form = new Dictionary<string, string>
            {
                { "channel", "whatsapp" },
                { "source", PrimeroNoVacio(request.Source, app.SourceNumber) },
                { "destination", SoloDigitos(request.Destination) },
                { "src.name", PrimeroNoVacio(request.SrcName, app.AppName) },
                { "message", JsonSerializer.Serialize(message) }
            };

            var result = await PostForm(app, "msg", form).ConfigureAwait(false);

            result.Destination = request.Destination;

            RegistrarEnvio(request.Tenant, "text", request.Destination, result);

            return result;
        }

        public async Task<GupshupApiResultDTO> OptIn(GupshupOptInRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Phone))
            {
                return GupshupApiResultDTO.Fail("El campo Phone es obligatorio.");
            }

            var app = _settings.ResolveApp(request.Tenant);

            var validacion = Validar(app, requiereOrigen: false);

            if (validacion != null)
            {
                return validacion;
            }

            if (string.IsNullOrWhiteSpace(app.AppId))
            {
                return GupshupApiResultDTO.Fail("No hay AppId configurado para la empresa.");
            }

            var form = new Dictionary<string, string> { { "user", SoloDigitos(request.Phone) } };

            return await PostForm(app, app.AppId + "/opt-in", form, usarUrlApp: true).ConfigureAwait(false);
        }

        public async Task<GupshupApiResultDTO> GetRemoteTemplates(string tenant)
        {
            var app = _settings.ResolveApp(tenant);

            var validacion = Validar(app, requiereOrigen: false);

            if (validacion != null)
            {
                return validacion;
            }

            if (string.IsNullOrWhiteSpace(app.AppId))
            {
                return GupshupApiResultDTO.Fail("No hay AppId configurado para la empresa.");
            }

            try
            {
                using var request = CrearPeticion(HttpMethod.Get, app, app.AppId + "/template", usarUrlApp: true);

                using var response = await Enviar(request).ConfigureAwait(false);

                var contenido = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return ConstruirResultado(response, contenido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando las plantillas de Gupshup para la empresa {Tenant}.", tenant);

                return GupshupApiResultDTO.Fail("Error consultando las plantillas en Gupshup: " + ex.Message);
            }
        }

        /// <summary>
        /// Interpreta un evento del webhook: resuelve la empresa, traduce el estado y
        /// deja la traza. La persistencia del estado queda a cargo de quien consuma el
        /// resultado, porque este modelo no tiene aun una tabla de mensajes enviados.
        /// </summary>
        public GupshupWebhookResultDTO ProcessWebhook(GupshupWebhookEvent webhookEvent, string tenant = null)
        {
            var result = new GupshupWebhookResultDTO
            {
                EventType = webhookEvent?.type,
                Tenant = PrimeroNoVacio(tenant, _settings.ResolveTenantByAppName(webhookEvent?.app))
            };

            if (webhookEvent?.payload == null)
            {
                result.Detail = "El evento no contiene payload.";

                return result;
            }

            result.MessageId = webhookEvent.payload.id;

            if (string.Equals(webhookEvent.type, "message", StringComparison.OrdinalIgnoreCase))
            {
                result.Phone = PrimeroNoVacio(webhookEvent.payload.source, webhookEvent.payload.sender?.phone);
                result.Text = webhookEvent.payload.Text;
                result.Handled = true;
                result.Detail = "Mensaje entrante recibido.";

                _logger.LogInformation(
                    "WhatsApp entrante. Empresa {Tenant}, telefono {Phone}, tipo {Tipo}.",
                    result.Tenant, result.Phone, webhookEvent.payload.type);

                return result;
            }

            if (!string.Equals(webhookEvent.type, "message-event", StringComparison.OrdinalIgnoreCase))
            {
                result.Detail = $"Evento '{webhookEvent.type}' recibido sin procesamiento de estado.";

                return result;
            }

            result.Phone = webhookEvent.payload.destination;
            result.MappedState = _settings.MapState(webhookEvent.payload.type);

            if (string.IsNullOrWhiteSpace(result.MappedState))
            {
                result.Detail =
                    $"El evento '{webhookEvent.payload.type}' no tiene estado mapeado en la configuracion.";

                return result;
            }

            result.Handled = true;
            result.Detail = "Estado del mensaje recibido.";

            _logger.LogInformation(
                "Estado WhatsApp. Empresa {Tenant}, mensaje {MessageId}, evento {Evento}, estado {Estado}, motivo {Motivo}.",
                result.Tenant, result.MessageId, webhookEvent.payload.type, result.MappedState,
                webhookEvent.payload.reason);

            return result;
        }

        public bool IsValidWebhookToken(string token)
        {
            if (string.IsNullOrWhiteSpace(_settings.WebhookToken))
            {
                return true;
            }

            return string.Equals(_settings.WebhookToken, token, StringComparison.Ordinal);
        }

        /// <summary>
        /// Resuelve los parametros posicionales de la plantilla. Params tiene prioridad;
        /// Values se ordena con los ParamNames de la plantilla configurada.
        /// </summary>
        private static List<string> ResolverParametros(
            GupshupTemplateRequestDTO request,
            WhatsappTemplateDTO plantilla)
        {
            if (request.Params != null && request.Params.Count > 0)
            {
                return request.Params.Select(x => x ?? string.Empty).ToList();
            }

            if (request.Values == null || request.Values.Count == 0)
            {
                return new List<string>();
            }

            // Sin ParamNames configurados no hay un orden confiable; se respeta el de llegada.
            if (plantilla?.ParamNames == null || plantilla.ParamNames.Count == 0)
            {
                return request.Values.Values.Select(x => x ?? string.Empty).ToList();
            }

            return plantilla.ParamNames
                .Select(nombre =>
                {
                    var item = request.Values.FirstOrDefault(
                        x => string.Equals(x.Key, nombre, StringComparison.OrdinalIgnoreCase));

                    return item.Value ?? string.Empty;
                })
                .ToList();
        }

        /// <summary>
        /// Arma el objeto "message" con la media de la cabecera segun el formato que
        /// espera Gupshup: image usa originalUrl/previewUrl; file, video y audio usan url.
        /// </summary>
        private static Dictionary<string, object> ResolverMedia(
            GupshupTemplateRequestDTO request,
            WhatsappTemplateDTO plantilla)
        {
            var tipo = PrimeroNoVacio(request.Media?.Type, plantilla?.HeaderType);
            var url = PrimeroNoVacio(request.Media?.Url, plantilla?.HeaderUrl);

            if (string.IsNullOrWhiteSpace(tipo)
                || string.Equals(tipo, "none", StringComparison.OrdinalIgnoreCase)
                || string.Equals(tipo, "text", StringComparison.OrdinalIgnoreCase)
                || string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            tipo = tipo.Trim().ToLowerInvariant();

            // WhatsApp lo llama document, Gupshup lo llama file.
            if (tipo == "document")
            {
                tipo = "file";
            }

            var media = new Dictionary<string, object> { { "type", tipo } };

            if (tipo == "image")
            {
                media.Add("originalUrl", url);
                media.Add("previewUrl", url);
            }
            else
            {
                media.Add("url", url);
            }

            var archivo = PrimeroNoVacio(request.Media?.Filename, plantilla?.HeaderFilename);

            if (tipo == "file" && !string.IsNullOrWhiteSpace(archivo))
            {
                media.Add("filename", archivo);
            }

            var caption = request.Media?.Caption;

            if (!string.IsNullOrWhiteSpace(caption) && tipo != "audio")
            {
                media.Add("caption", caption);
            }

            return media;
        }

        private GupshupApiResultDTO Validar(GupshupAppSettings app, bool requiereOrigen = true)
        {
            if (!app.Enabled)
            {
                return GupshupApiResultDTO.Fail(
                    $"La integracion con Gupshup esta deshabilitada para la empresa '{app.Tenant}'.");
            }

            if (string.IsNullOrWhiteSpace(app.ApiKey))
            {
                return GupshupApiResultDTO.Fail("No hay ApiKey de Gupshup configurada.");
            }

            if (requiereOrigen && string.IsNullOrWhiteSpace(app.SourceNumber))
            {
                return GupshupApiResultDTO.Fail("No hay numero origen (SourceNumber) configurado.");
            }

            if (requiereOrigen && string.IsNullOrWhiteSpace(app.AppName))
            {
                return GupshupApiResultDTO.Fail("No hay nombre de App (AppName) configurado.");
            }

            return null;
        }

        private async Task<GupshupApiResultDTO> PostForm(
            GupshupAppSettings app,
            string path,
            Dictionary<string, string> form,
            bool usarUrlApp = false)
        {
            try
            {
                using var request = CrearPeticion(HttpMethod.Post, app, path, usarUrlApp);

                request.Content = new FormUrlEncodedContent(
                    form.Where(x => !string.IsNullOrWhiteSpace(x.Value)));

                using var response = await Enviar(request).ConfigureAwait(false);

                var cuerpo = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return ConstruirResultado(response, cuerpo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invocando el API de Gupshup en {Path}.", path);

                return GupshupApiResultDTO.Fail("Error invocando el API de Gupshup: " + ex.Message);
            }
        }

        private HttpRequestMessage CrearPeticion(
            HttpMethod method,
            GupshupAppSettings app,
            string path,
            bool usarUrlApp = false)
        {
            var baseUrl = usarUrlApp
                ? PrimeroNoVacio(_settings.AppUrl, UrlAppPorDefecto)
                : PrimeroNoVacio(_settings.BaseUrl, UrlBasePorDefecto);

            if (!baseUrl.EndsWith("/", StringComparison.Ordinal))
            {
                baseUrl += "/";
            }

            var request = new HttpRequestMessage(method, new Uri(new Uri(baseUrl), path));

            request.Headers.Add("apikey", app.ApiKey);
            request.Headers.Add("Accept", "application/json");

            return request;
        }

        /// <summary>
        /// Envia la peticion aplicando el timeout configurado mediante cancelacion, sin
        /// alterar el HttpClient que entrega la fabrica.
        /// </summary>
        private async Task<HttpResponseMessage> Enviar(HttpRequestMessage request)
        {
            var segundos = _settings.TimeoutSeconds > 0 ? _settings.TimeoutSeconds : 30;

            using var cancelacion = new CancellationTokenSource(TimeSpan.FromSeconds(segundos));

            var cliente = _clientFactory.CreateClient();

            return await cliente.SendAsync(request, cancelacion.Token).ConfigureAwait(false);
        }

        private static GupshupApiResultDTO ConstruirResultado(HttpResponseMessage response, string cuerpo)
        {
            var result = new GupshupApiResultDTO
            {
                Success = response.IsSuccessStatusCode,
                HttpStatusCode = (int)response.StatusCode,
                RawResponse = cuerpo
            };

            if (!string.IsNullOrWhiteSpace(cuerpo) && cuerpo.TrimStart().StartsWith("{", StringComparison.Ordinal))
            {
                try
                {
                    var parsed = JsonSerializer.Deserialize<GupshupSendResponse>(cuerpo, JsonOptions);

                    result.Status = parsed?.status;
                    result.MessageId = parsed?.messageId;

                    if (!result.Success)
                    {
                        result.ErrorMessage = parsed?.message ?? cuerpo;
                    }
                }
                catch (JsonException)
                {
                    // Gupshup no siempre responde con el mismo esquema; si no se puede
                    // interpretar queda el cuerpo crudo para diagnostico.
                    result.ErrorMessage = result.Success ? null : cuerpo;
                }
            }
            else if (!result.Success)
            {
                result.ErrorMessage = cuerpo;
            }

            return result;
        }

        private void RegistrarEnvio(string tenant, string tipo, string destino, GupshupApiResultDTO result)
        {
            if (result.Success)
            {
                _logger.LogInformation(
                    "Envio WhatsApp {Tipo} aceptado. Empresa {Tenant}, destino {Destino}, mensaje {MessageId}, estado {Estado}.",
                    tipo, tenant, destino, result.MessageId, result.Status);
            }
            else
            {
                _logger.LogError(
                    "Envio WhatsApp {Tipo} rechazado. Empresa {Tenant}, destino {Destino}, http {Http}, error {Error}.",
                    tipo, tenant, destino, result.HttpStatusCode, result.ErrorMessage);
            }
        }

        private static string SoloDigitos(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? value
                : new string(value.Where(char.IsDigit).ToArray());
        }

        private static string PrimeroNoVacio(params string[] values)
        {
            return values?.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
