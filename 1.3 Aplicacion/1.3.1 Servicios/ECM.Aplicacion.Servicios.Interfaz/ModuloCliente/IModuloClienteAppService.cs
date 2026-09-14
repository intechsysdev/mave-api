using ECM.Aplicacion.DTO.Cliente;
using Itdear.Aplicacion.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.ModuloCliente
{
    public interface IModuloClienteAppService
    {
        #region Cartera
        Task<IEnumerable<CarteraDTO>> ConsultarCarteraCliente(int? mesesCartera);
        Task<ConsultaViewModel<CarteraDTO>> ConsultarCarteraCliente(int? mesesCartera, string textoBusqueda, int pagina, int registrosPorPagina, string ordenarPor, bool direccionOrdenamientoAsc);

        #endregion

        #region Menu Usuario
        Task<IEnumerable<MenuDTO>> ConsultarMenuUsuario();

        #endregion
    }
}
