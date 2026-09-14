using ECM.Aplicacion.DTO.ModuloMob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.ModuloMob
{
    public interface IGestionModAppService
    {
        Task<IEnumerable<MobCarteraDTO>> ConsultarCarteraCliente(string cmpy, string cust, string succli);

        Task<IEnumerable<MobCarteraDTO>> ConsultarCarteraCliente(string cmpy, string cust, string succli, int mesesCartera);

        Task<IEnumerable<MobCalendarioDTO>> ConsultarCalendarioHoy(string cmpy);

        Task<IEnumerable<MobEventosItemsDTO>> ConsultarEventosItemsHoy(string cmpy, string type, string id);

        Task<IEnumerable<MobEventosItemsDTO>> ConsultarEventosItemsHoy(string cmpy, string type);

        Task<IEnumerable<MobPromocionesDTO>> ConsultarPromocionesHoy(string cmpy);

        Task<MobRcustConsecDTO> ConsultarConsecutivoCliente(string cmpy, string cust, string succli, string tipdoc);

        Task<MobRcustConsecDTO> CrearConsecutivoCliente(MobRcustConsecDTO item);
        
        Task<MobRcustUserDTO> ConsultarClienteUsuario(string cmpy, string cust, string succli);

        Task<MobRcustUserDTO> ActualizarClienteUsuario(MobRcustUserDTO item);

        Task ActualizarDetallesOrden(IEnumerable<MobRordLineCustDTO> lineas);

        IEnumerable<MobRlineaDTO> ConsultarLineasCliente(string cmpy, string cust, string succli);

        IEnumerable<MobRsubLineasDTO> ConsultarSubLineasCliente(string cmpy, string cust, string succli);

        Task<MobRperfilesDTO> ConsultarPerfilUsuario(string cmpy, string codper);
        
        Task<IEnumerable<MobRprodCustDTO>> ConsultarProductosCliente(string cmpy, string cust, string succli);

        Task<MobRproductosDTO> ConsultarProducto(string cmpy, string id);


        #region Ordenes

        Task<IEnumerable<MobRordHeadCustDTO>> ConsultarEncabezadosOrdenes(string cmpy, string codusu, string ship, int hist);

        //MobRordHeadCustDTO CrearEncabezadoOrden(MobRordHeadCustDTO orden);
        Task<MobRordHeadCustDTO> CrearEncabezadoOrden(MobRordHeadCustDTO orden);

        Task<MobRordHeadCustDTO> ActualizarEncabezadoOrden(MobRordHeadCustDTO orden);

        Task EliminarEncabezadoOrden(MobRordHeadCustDTO orden);

        #endregion

        #region Orden Linea

        Task<IEnumerable<MobRordLineCustDTO>> ConsultarDetalleOrden(int num, string cust, string ship);

        Task<MobRordLineCustDTO> CrearDetalleOrden(MobRordLineCustDTO linea);

        Task CrearDetallesOrden(IEnumerable<MobRordLineCustDTO> lineas);

        Task<MobRordLineCustDTO> ActualizarDetalleOrden(MobRordLineCustDTO orden);

        Task EliminarDetalleOrden(MobRordLineCustDTO orden);

        #endregion
    }
}
