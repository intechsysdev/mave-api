using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ECM.Aplicacion.DTO.Gupshup;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECM.Aplicacion.Servicios.Messages
{
    /// <summary>
    /// Catalogo de plantillas respaldado por un archivo json.
    ///
    /// El contenido se cachea en memoria porque se consulta en cada envio, y se protege
    /// con un lock porque el endpoint de administracion permite modificarlo en caliente.
    /// </summary>
    public class WhatsappTemplateData : IWhatsappTemplateData
    {
        private const string RutaPorDefecto = "Data/WhatsappTemplates.json";

        private static readonly object SyncRoot = new object();

        private static readonly JsonSerializerOptions JsonLectura = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        private static readonly JsonSerializerOptions JsonEscritura = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private static List<WhatsappTemplateDTO> _cache;

        private readonly ILogger _logger;

        private readonly string _rutaArchivo;

        public WhatsappTemplateData(ILogger<WhatsappTemplateData> logger, IConfiguration configuration)
        {
            _logger = logger;

            var configurada = configuration.GetSection("Gupshup:TemplatesFile").Get<string>();

            var relativa = string.IsNullOrWhiteSpace(configurada) ? RutaPorDefecto : configurada;

            _rutaArchivo = Path.IsPathRooted(relativa)
                ? relativa
                : Path.Combine(AppContext.BaseDirectory, relativa);
        }

        public IEnumerable<WhatsappTemplateDTO> GetAll()
        {
            return Cargar().ToList();
        }

        public IEnumerable<WhatsappTemplateDTO> GetAll(string tenant)
        {
            return Cargar()
                .Where(x => string.Equals(x.Tenant, tenant, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public WhatsappTemplateDTO Get(string tenant, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return Cargar().FirstOrDefault(x =>
                string.Equals(x.Tenant, tenant, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public WhatsappTemplateDTO Save(WhatsappTemplateDTO template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (string.IsNullOrWhiteSpace(template.Name) || string.IsNullOrWhiteSpace(template.Tenant))
            {
                throw new ArgumentException("La plantilla debe indicar Tenant y Name.", nameof(template));
            }

            lock (SyncRoot)
            {
                var plantillas = Cargar().ToList();

                plantillas.RemoveAll(x =>
                    string.Equals(x.Tenant, template.Tenant, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(x.Name, template.Name, StringComparison.OrdinalIgnoreCase));

                template.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                plantillas.Add(template);

                Guardar(plantillas);
            }

            return template;
        }

        public bool Delete(string tenant, string name)
        {
            lock (SyncRoot)
            {
                var plantillas = Cargar().ToList();

                var eliminadas = plantillas.RemoveAll(x =>
                    string.Equals(x.Tenant, tenant, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

                if (eliminadas == 0)
                {
                    return false;
                }

                Guardar(plantillas);

                return true;
            }
        }

        public void Reload()
        {
            lock (SyncRoot)
            {
                _cache = null;
            }
        }

        private List<WhatsappTemplateDTO> Cargar()
        {
            if (_cache != null)
            {
                return _cache;
            }

            lock (SyncRoot)
            {
                if (_cache != null)
                {
                    return _cache;
                }

                if (!File.Exists(_rutaArchivo))
                {
                    _logger.LogWarning(
                        "No se encontro el archivo de plantillas de WhatsApp en {Ruta}. Se trabaja con el catalogo vacio.",
                        _rutaArchivo);

                    return _cache = new List<WhatsappTemplateDTO>();
                }

                try
                {
                    var contenido = File.ReadAllText(_rutaArchivo);

                    _cache = JsonSerializer.Deserialize<List<WhatsappTemplateDTO>>(contenido, JsonLectura)
                             ?? new List<WhatsappTemplateDTO>();
                }
                catch (Exception ex)
                {
                    // Un archivo mal formado no debe impedir el arranque ni tumbar un envio
                    // por id de plantilla, que no depende del catalogo.
                    _logger.LogError(ex, "Error leyendo el archivo de plantillas de WhatsApp {Ruta}.", _rutaArchivo);

                    _cache = new List<WhatsappTemplateDTO>();
                }

                return _cache;
            }
        }

        private void Guardar(List<WhatsappTemplateDTO> plantillas)
        {
            var carpeta = Path.GetDirectoryName(_rutaArchivo);

            if (!string.IsNullOrEmpty(carpeta) && !Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            File.WriteAllText(_rutaArchivo, JsonSerializer.Serialize(plantillas, JsonEscritura));

            _cache = plantillas;
        }
    }
}
