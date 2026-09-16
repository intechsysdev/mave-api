using Microsoft.Extensions.Logging;

namespace Itdear.Infraestructura.Transversal.Adaptador
{
    /// <summary>
    /// Fachada de log independiente del proveedor. Permite que las capas de dominio y
    /// aplicacion registren trazas sin acoplarse a Microsoft.Extensions.Logging.
    /// </summary>
    public interface IAppLogger<T>
    {
        void LogTrace(string message, params object[] args);

        void LogDebug(string message, params object[] args);

        void LogInformation(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogError(System.Exception exception, string message, params object[] args);

        void LogCritical(string message, params object[] args);

        void LogCritical(System.Exception exception, string message, params object[] args);
    }

    /// <summary>
    /// Implementacion sobre <see cref="ILogger{TCategoryName}"/>.
    /// </summary>
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogTrace(string message, params object[] args) => _logger.LogTrace(message, args);

        public void LogDebug(string message, params object[] args) => _logger.LogDebug(message, args);

        public void LogInformation(string message, params object[] args) => _logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args) => _logger.LogWarning(message, args);

        public void LogError(string message, params object[] args) => _logger.LogError(message, args);

        public void LogError(System.Exception exception, string message, params object[] args)
            => _logger.LogError(exception, message, args);

        public void LogCritical(string message, params object[] args) => _logger.LogCritical(message, args);

        public void LogCritical(System.Exception exception, string message, params object[] args)
            => _logger.LogCritical(exception, message, args);
    }
}
