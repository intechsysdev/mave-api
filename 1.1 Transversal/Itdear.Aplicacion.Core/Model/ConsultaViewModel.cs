using System.Collections.Generic;
using System.Linq;

namespace Itdear.Aplicacion.Core.Model
{
    /// <summary>
    /// Resultado de una consulta paginada: la pagina solicitada junto con el total de
    /// registros que cumplen el filtro, que es lo que el cliente necesita para pintar
    /// el paginador.
    /// </summary>
    public class ConsultaViewModel<T>
    {
        public ConsultaViewModel()
        {
            Items = Enumerable.Empty<T>();
        }

        /// <summary>Total de registros que cumplen el filtro, sin paginar.</summary>
        public int TotalRegistros { get; set; }

        /// <summary>Registros de la pagina solicitada.</summary>
        public IEnumerable<T> Items { get; set; }
    }
}
