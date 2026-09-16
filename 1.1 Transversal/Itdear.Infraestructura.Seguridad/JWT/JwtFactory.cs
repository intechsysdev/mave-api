using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Itdear.Infraestructura.Seguridad.JWT
{
    /// <summary>
    /// Emisor de los tokens de acceso de la API.
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// Emite el token codificado para el usuario indicado.
        /// </summary>
        /// <param name="user">Usuario autenticado. Solo se usa para trazabilidad, los datos viajan en los claims.</param>
        /// <param name="identity">Claims que se incorporan al token.</param>
        /// <param name="refreshToken">Token de refresco asociado a la sesion.</param>
        string GenerateEncodedToken(object user, ClaimsIdentity identity, string refreshToken);

        /// <summary>Emite el token a partir de los claims, sin usuario asociado.</summary>
        string GenerateEncodedToken(ClaimsIdentity identity);
    }

    /// <summary>
    /// Implementacion sobre System.IdentityModel.Tokens.Jwt.
    /// </summary>
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtOptions _jwtOptions;

        public JwtFactory(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;

            if (_jwtOptions.ValidForMinutes <= 0)
            {
                throw new ArgumentException(
                    "JwtOptions.ValidForMinutes debe ser mayor que cero.", nameof(jwtOptions));
            }

            if (_jwtOptions.SigningCredentials == null)
            {
                throw new ArgumentException(
                    "JwtOptions.SigningCredentials no puede ser nulo.", nameof(jwtOptions));
            }
        }

        public string GenerateEncodedToken(object user, ClaimsIdentity identity, string refreshToken)
        {
            var claims = BuildClaims(identity);

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                claims.Add(new Claim("refresh_token", refreshToken));
            }

            return Encode(claims);
        }

        public string GenerateEncodedToken(ClaimsIdentity identity)
        {
            return Encode(BuildClaims(identity));
        }

        private List<Claim> BuildClaims(ClaimsIdentity identity)
        {
            var claims = new List<Claim>();

            if (identity != null)
            {
                claims.AddRange(identity.Claims);
            }

            // El jti lo aporta quien construye la identidad (guarda el id de sesion);
            // solo se genera aqui cuando no viene, para que el token nunca quede sin el.
            if (claims.All(c => c.Type != JwtRegisteredClaimNames.Jti))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            }

            claims.Add(new Claim(
                JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(System.Globalization.CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer64));

            if (!string.IsNullOrWhiteSpace(_jwtOptions.Subject))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Subject));
            }

            return claims;
        }

        private string Encode(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds);
        }
    }
}
