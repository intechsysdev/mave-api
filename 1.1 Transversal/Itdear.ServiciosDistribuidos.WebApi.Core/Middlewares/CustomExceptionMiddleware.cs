using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Itdear.Infraestructura.Transversal.Exception;
using Itdear.ServiciosDistribuidos.WebApi.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Itdear.ServiciosDistribuidos.WebApi.Core.Middlewares
{
    /// <summary>
    /// Traduce las excepciones que llegan sin controlar a una respuesta json uniforme.
    ///
    /// Los errores de negocio conservan su mensaje porque estan redactados para el
    /// usuario final; cualquier otra excepcion se registra completa en el log pero se
    /// responde con un texto generico para no filtrar detalles de la implementacion.
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private const string MensajeErrorNoControlado =
            "Se presento un error inesperado. Intente de nuevo; si el problema persiste contacte con soporte.";

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        private readonly RequestDelegate _next;

        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida en {Path}: {Message}", context.Request.Path, ex.Message);

                await WriteAsync(context, HttpStatusCode.BadRequest, ex.Message, null).ConfigureAwait(false);
            }
            catch (BadRequestCustomException ex)
            {
                _logger.LogWarning(ex, "Peticion invalida en {Path}: {Message}", context.Request.Path, ex.Message);

                await WriteAsync(context, HttpStatusCode.BadRequest, ex.Title ?? ex.Message, ex.Detail)
                    .ConfigureAwait(false);
            }
            catch (NotFoundCustomException ex)
            {
                _logger.LogWarning(ex, "Recurso no encontrado en {Path}: {Message}", context.Request.Path, ex.Message);

                await WriteAsync(context, HttpStatusCode.NotFound, ex.Title ?? ex.Message, ex.Detail)
                    .ConfigureAwait(false);
            }
            catch (ForbiddenCustomException ex)
            {
                _logger.LogWarning(ex, "Acceso denegado en {Path}: {Message}", context.Request.Path, ex.Message);

                await WriteAsync(context, HttpStatusCode.Forbidden, ex.Title ?? ex.Message, ex.Detail)
                    .ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en {Path}", context.Request.Path);

                await WriteAsync(context, HttpStatusCode.InternalServerError, MensajeErrorNoControlado, null)
                    .ConfigureAwait(false);
            }
        }

        private static async Task WriteAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string title,
            string detail)
        {
            // Si la respuesta ya empezo a enviarse no se puede cambiar el codigo ni el
            // cuerpo; en ese caso solo queda dejar la traza en el log.
            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";

            var body = new ErrorViewModel
            {
                Title = title,
                Detail = detail,
                Status = (int)statusCode,
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(body, JsonOptions)).ConfigureAwait(false);
        }
    }
}
