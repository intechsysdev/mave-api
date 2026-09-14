using ECM.Dominio.ModuloSeg.Repositories;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using Itdear.Infraestructura.Transversal.Exception;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Microsoft.Extensions.Configuration;

namespace ECM.Aplicacion.Servicios.Messages
{
    public class MailService : IMailService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _urlService;

        public MailService(
            ILogger<MailService> logger,
            IEcmMempresaRepository ecmMempresaRepository,
            IHttpClientFactory clientFactory,
            IConfiguration configuration
           )
        {
            _logger = logger;
            _clientFactory = clientFactory;

            _configuration = configuration;

            _urlService = _configuration.GetSection("UrlServiceMail").Get<string>();

            if (string.IsNullOrEmpty(_urlService))
            {
                _logger.LogError("No se a configurado UrlServiceSMS en el config del servicio");
            }
        }

      
        public async Task Send(string keyApp, string from, string to, string subject, int templateId, object defaultPlaceholders, bool intermediateReport = true, bool generateException = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _urlService);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("App", keyApp.Trim());
            
            var jsonParameters = JsonConvert.SerializeObject(defaultPlaceholders);

            var content = new MultipartFormDataContent
            {
                { new StringContent(from), "from" },
                { new StringContent(to), "to" },
                { new StringContent(subject), "subject" },
                { new StringContent(templateId.ToString()), "templateid" },
                { new StringContent(jsonParameters), "defaultPlaceholders" },
                { new StringContent(intermediateReport.ToString()), "intermediateReport" },
                { new StringContent("application/json"), "notifyContentType" },
            };

            request.Content = content;

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var result = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {

                _logger.LogInformation("SendMail '{0}'. From {1}, to {2}, Result {3}", response.StatusCode, from, to, result);

            }
            else
            {
                _logger.LogError("SendMail error '{0}'. From {1}, to {2}, subject {3}, templateId {4} defaultPlaceholders '{5}'", response.StatusCode, from, to, subject, templateId, jsonParameters);

                if (generateException)
                {
                    throw new BadRequestCustomException("Error envío correo", "Se generó un error en el envío del correo, intente de nuevo si el problema persiste contacte con soporte");
                }
            }           
        }
    }
}
