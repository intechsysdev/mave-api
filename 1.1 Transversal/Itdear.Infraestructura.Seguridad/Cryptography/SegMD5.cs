using System;
using System.Security.Cryptography;
using System.Text;

namespace Itdear.Infraestructura.Seguridad.Cryptography
{
    /// <summary>
    /// Hash MD5 en hexadecimal minusculas.
    ///
    /// Se conserva el algoritmo original porque las contrasenas y los tokens ya
    /// almacenados en la base se generaron con el; cambiarlo invalidaria las
    /// credenciales existentes. No debe usarse para cifrar informacion nueva.
    /// </summary>
    public class SegMD5
    {
        /// <summary>
        /// Devuelve el hash MD5 del texto recibido como cadena hexadecimal en minusculas.
        /// Para una entrada nula o vacia devuelve cadena vacia.
        /// </summary>
        public string GetMd5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var data = MD5.HashData(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder(data.Length * 2);

            foreach (var item in data)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>Compara un texto plano contra un hash previamente calculado.</summary>
        public bool VerifyMd5Hash(string input, string hash)
        {
            return string.Equals(GetMd5Hash(input), hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
