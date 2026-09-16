namespace Itdear.Infraestructura.Transversal.Exception
{
    /// <summary>
    /// Error de negocio previsto. El middleware lo traduce a 400 con el mensaje tal
    /// como se escribio, porque esta redactado para el usuario final.
    /// </summary>
    public class ValidationException : System.Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Error que se devuelve como 400 separando el titulo del detalle. Se usa cuando
    /// falla una dependencia externa y hay que dar un mensaje amable al usuario.
    /// </summary>
    public class BadRequestCustomException : System.Exception
    {
        public BadRequestCustomException()
        {
        }

        public BadRequestCustomException(string message) : base(message)
        {
            Title = message;
        }

        public BadRequestCustomException(string title, string detail) : base(detail)
        {
            Title = title;
            Detail = detail;
        }

        public BadRequestCustomException(string title, string detail, System.Exception innerException)
            : base(detail, innerException)
        {
            Title = title;
            Detail = detail;
        }

        /// <summary>Titulo corto del error.</summary>
        public string Title { get; set; }

        /// <summary>Descripcion ampliada del error.</summary>
        public string Detail { get; set; }
    }

    /// <summary>
    /// Recurso inexistente. El middleware lo traduce a 404.
    /// </summary>
    public class NotFoundCustomException : System.Exception
    {
        public NotFoundCustomException()
        {
        }

        public NotFoundCustomException(string message) : base(message)
        {
            Title = message;
        }

        public NotFoundCustomException(string title, string detail) : base(detail)
        {
            Title = title;
            Detail = detail;
        }

        public NotFoundCustomException(string title, string detail, System.Exception innerException)
            : base(detail, innerException)
        {
            Title = title;
            Detail = detail;
        }

        public string Title { get; set; }

        public string Detail { get; set; }
    }

    /// <summary>
    /// Acceso no permitido al recurso solicitado. El middleware lo traduce a 403.
    /// </summary>
    public class ForbiddenCustomException : System.Exception
    {
        public ForbiddenCustomException()
        {
        }

        public ForbiddenCustomException(string message) : base(message)
        {
            Title = message;
        }

        public ForbiddenCustomException(string title, string detail) : base(detail)
        {
            Title = title;
            Detail = detail;
        }

        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
