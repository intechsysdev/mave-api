using System.Collections.Generic;

namespace Itdear.ServiciosDistribuidos.WebApi.Core.ViewModels
{
    /// <summary>
    /// Cuerpo con el que la API responde cuando una peticion termina en error.
    /// </summary>
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string title, string detail = null)
        {
            Title = title;
            Detail = detail;
        }

        /// <summary>Mensaje corto, apto para mostrarse al usuario.</summary>
        public string Title { get; set; }

        /// <summary>Descripcion ampliada del error.</summary>
        public string Detail { get; set; }

        /// <summary>Codigo http de la respuesta.</summary>
        public int Status { get; set; }

        /// <summary>Identificador de la peticion, para poder cruzarla con el log.</summary>
        public string TraceId { get; set; }

        /// <summary>Errores de validacion por campo, cuando aplican.</summary>
        public IDictionary<string, string[]> Errors { get; set; }
    }

    /// <summary>
    /// Datos de paginacion que acompanan a una consulta.
    /// </summary>
    public class PaginacionViewModel
    {
        /// <summary>Pagina solicitada, base 1.</summary>
        public int Pagina { get; set; } = 1;

        /// <summary>Cantidad de registros por pagina.</summary>
        public int RegistrosPorPagina { get; set; } = 20;

        /// <summary>Campo por el que se ordena.</summary>
        public string OrdenarPor { get; set; }

        /// <summary>Indica si el ordenamiento es ascendente.</summary>
        public bool DireccionOrdenamientoAsc { get; set; } = true;

        /// <summary>Texto libre de busqueda.</summary>
        public string TextoBusqueda { get; set; }
    }
}
