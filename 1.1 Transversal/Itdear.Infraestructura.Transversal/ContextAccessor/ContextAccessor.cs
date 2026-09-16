using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Itdear.Infraestructura.Transversal.ContextAccessor
{
    /// <summary>
    /// Implementacion sobre <see cref="IHttpContextAccessor"/>. Todas las propiedades
    /// devuelven cadena vacia cuando no hay peticion en curso (por ejemplo en tareas
    /// de arranque), para que el consumidor no tenga que validar nulos.
    /// </summary>
    public class ContextAccessor : IContextAccessor
    {
        /// <summary>Cabeceras aceptadas para la version de la aplicacion cliente.</summary>
        private static readonly string[] AppVersionHeaders = { "AppVersion", "App-Version", "X-App-Version" };

        /// <summary>Cabeceras aceptadas para el tipo de aplicacion cliente.</summary>
        private static readonly string[] AppTypeHeaders = { "AppType", "App-Type", "X-App-Type" };

        /// <summary>Cabeceras de proxy que conservan la direccion original del cliente.</summary>
        private static readonly string[] ForwardedForHeaders = { "X-Forwarded-For", "X-Real-IP" };

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpContext Context => _httpContextAccessor?.HttpContext;

        public string ClientIP
        {
            get
            {
                var context = Context;

                if (context == null)
                {
                    return string.Empty;
                }

                foreach (var header in ForwardedForHeaders)
                {
                    var value = ReadHeader(context, header);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        // X-Forwarded-For puede traer la cadena completa de proxies.
                        return value.Split(',')[0].Trim();
                    }
                }

                return context.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            }
        }

        public int AppVersion => ReadFirstHeaderAsInt(AppVersionHeaders);

        public int AppType => ReadFirstHeaderAsInt(AppTypeHeaders);

        public string CompanyId => ReadClaim("CompanyId");

        public string UserCust => ReadClaim("Cust");

        public string UserSuccli => ReadClaim("Succli");

        public string UserName => ReadClaim("unique_name", ClaimTypes.Name);

        public string Email => ReadClaim("email", ClaimTypes.Email);

        public string SessionId => ReadClaim("jti");

        public bool IsAuthenticated => Context?.User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Lee la primera cabecera informada y la convierte a entero. Un valor ausente o
        /// no numerico se trata como 0 para no tumbar la peticion por una cabecera mal
        /// formada del cliente.
        /// </summary>
        private int ReadFirstHeaderAsInt(string[] headerNames)
        {
            var value = ReadFirstHeader(headerNames);

            return int.TryParse(value, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out var result)
                ? result
                : 0;
        }

        private string ReadFirstHeader(string[] headerNames)
        {
            var context = Context;

            if (context == null)
            {
                return string.Empty;
            }

            foreach (var header in headerNames)
            {
                var value = ReadHeader(context, header);

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        private static string ReadHeader(HttpContext context, string name)
        {
            return context.Request != null && context.Request.Headers.TryGetValue(name, out var value)
                ? value.ToString()
                : string.Empty;
        }

        private string ReadClaim(params string[] claimTypes)
        {
            var user = Context?.User;

            if (user == null)
            {
                return string.Empty;
            }

            foreach (var claimType in claimTypes)
            {
                var claim = user.Claims.FirstOrDefault(
                    c => string.Equals(c.Type, claimType, StringComparison.OrdinalIgnoreCase));

                if (claim != null)
                {
                    return claim.Value;
                }
            }

            return string.Empty;
        }
    }
}
