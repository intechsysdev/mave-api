namespace Itdear.Infraestructura.Transversal.ContextAccessor
{
    /// <summary>
    /// Datos del usuario y del dispositivo que originan la peticion en curso.
    /// Se resuelven de los claims del JWT y de las cabeceras de la peticion, de modo
    /// que los servicios de aplicacion no dependan de ASP.NET Core.
    /// </summary>
    public interface IContextAccessor
    {
        /// <summary>Direccion del cliente. Se usa como identificador del dispositivo (columna MAC).</summary>
        string ClientIP { get; }

        /// <summary>
        /// Version de la aplicacion cliente, tomada de la cabecera correspondiente.
        /// Devuelve 0 cuando el cliente no la informa.
        /// </summary>
        int AppVersion { get; }

        /// <summary>
        /// Tipo de aplicacion cliente (web, movil, ...), tomado de la cabecera
        /// correspondiente. Devuelve 0 cuando el cliente no lo informa.
        /// </summary>
        int AppType { get; }

        /// <summary>Empresa del usuario autenticado (claim CompanyId).</summary>
        string CompanyId { get; }

        /// <summary>Codigo de cliente del usuario autenticado (claim Cust).</summary>
        string UserCust { get; }

        /// <summary>Sucursal del usuario autenticado (claim Succli).</summary>
        string UserSuccli { get; }

        /// <summary>Nombre del usuario autenticado.</summary>
        string UserName { get; }

        /// <summary>Correo del usuario autenticado.</summary>
        string Email { get; }

        /// <summary>Identificador de la sesion (claim jti).</summary>
        string SessionId { get; }

        /// <summary>Indica si la peticion llega autenticada.</summary>
        bool IsAuthenticated { get; }
    }
}
