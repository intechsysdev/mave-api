using System;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.Extensions.Logging;

namespace Itdear.Aplicacion.Core
{
    /// <summary>
    /// Base de los servicios de aplicacion. Centraliza las dos dependencias que
    /// necesitan practicamente todos: el contexto de la peticion y el log.
    /// </summary>
    public abstract class BaseAppService
    {
        protected BaseAppService(IContextAccessor contextAccessor, ILoggerFactory loggerFactory)
        {
            ContextAccessor = contextAccessor;

            Logger = loggerFactory?.CreateLogger(GetType().FullName);
        }

        /// <summary>Datos del usuario y del dispositivo que originan la peticion.</summary>
        protected IContextAccessor ContextAccessor { get; }

        /// <summary>Log con la categoria del servicio concreto.</summary>
        protected ILogger Logger { get; }
    }
}
