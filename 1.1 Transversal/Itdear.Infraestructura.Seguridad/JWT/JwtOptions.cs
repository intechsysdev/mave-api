using System;
using Microsoft.IdentityModel.Tokens;

namespace Itdear.Infraestructura.Seguridad.JWT
{
    /// <summary>
    /// Parametros de emision del token. Se enlazan desde la seccion JwtOptions del
    /// appsettings, salvo las credenciales de firma, que se arman en el arranque a
    /// partir de la clave secreta.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>Emisor del token (claim iss).</summary>
        public string Issuer { get; set; }

        /// <summary>Destinatario del token (claim aud).</summary>
        public string Audience { get; set; }

        /// <summary>Sujeto del token (claim sub).</summary>
        public string Subject { get; set; }

        /// <summary>Vigencia del token en minutos.</summary>
        public int ValidForMinutes { get; set; }

        /// <summary>Credenciales con las que se firma el token.</summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>Instante a partir del cual el token es valido.</summary>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>Instante de emision del token.</summary>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>Vigencia como intervalo.</summary>
        public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForMinutes);

        /// <summary>Instante de expiracion, calculado sobre <see cref="NotBefore"/>.</summary>
        public DateTime Expiration => NotBefore.Add(ValidFor);
    }
}
