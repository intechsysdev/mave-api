using ECM.Dominio.ModuloSeg.Repositories;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Itdear.Infraestructura.Transversal.Exception;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace ECM.Aplicacion.Servicios.Messages
{
    public class SmsService : ISmsService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _urlService;

        public SmsService(
           ILogger<ISmsService> logger,
           IContextAccessor contextAccessor,
           IHttpClientFactory clientFactory,
           IConfiguration configuration
           )
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _clientFactory = clientFactory;
            _configuration = configuration;

            _urlService = _configuration.GetSection("UrlServiceSMS").Get<string>();

            if (string.IsNullOrEmpty(_urlService)) {
                _logger.LogError("No se a configurado UrlServiceSMS en el config del servicio");
            }
        }

        public async Task Send(string keyApi, string from, string to, string text, bool generateException = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _urlService);

            request.Headers.Authorization = new AuthenticationHeaderValue("App", keyApi.Trim());
        
            var json = JsonConvert.SerializeObject(new { from, to, text });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            request.Content = stringContent;

            var cliente = _clientFactory.CreateClient();

            var response = await cliente.SendAsync(request);

            var result = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode) {

                _logger.LogInformation("SendSms '{0}'. From {1}, to {2}, Result {3}", response.StatusCode, from, to, result);

            } else
            {
                _logger.LogError("SendSms error '{0}'. From {1}, to {2}, text {3}", $"{response.StatusCode}: {result}", from, to, text);

                if (generateException)
                {
                    throw new BadRequestCustomException("Error envío sms", "Se generó un error en el envío del sms, intente de nuevo si el problema persiste contacte con soporte");
                }
            }
        }
    }
}
