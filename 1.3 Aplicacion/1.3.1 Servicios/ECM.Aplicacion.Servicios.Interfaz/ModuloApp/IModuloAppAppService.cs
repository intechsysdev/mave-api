using ECM.Aplicacion.DTO.ModuloApp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.ModuloApp
{
    public interface IModuloAppAppService
    {
        #region Aplicación

        Task<IEnumerable<NovedadAppDTO>> ConsultarNovedades();

        #endregion

        #region Empresa

        Task<IEnumerable<ContactoEmpresaDTO>> ConsultarContactosEmpresa();

        #endregion

        Task AceptarNovedades(int idNovedad);

        #region Terminos y condiciones
        Task AceptarTerminosYCondiciones();

        #endregion
    }
}
