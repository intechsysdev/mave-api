using ECM.Aplicacion.DTO.Ecommerce;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce
{
    public interface IModuloEcommerceAppService
    {
        #region Producto
        Task<IEnumerable<ProductoDTO>> ConsultarProductosCliente();

        Task<IEnumerable<LineaDTO>> ConsultarLineasCliente();

        #endregion

        #region Promoción
        Task<IEnumerable<PromocionDTO>> ConsultarPromociones();

        #endregion

        #region Carro 

        Task ActualizarProductoCarro(ProductoDTO producto);

        Task<IEnumerable<ProductoDTO>> ConsultarProductosCarro();

        #endregion

        #region Pedido

        Task<int> CrearPedido(PedidoDTO pedido);

        Task<PedidoDTO> ActualizarPedido(PedidoDTO pedido);

        Task<PedidoDTO> ConsultarPedido(int idPeddo);

        Task<PedidoDTO> ConsultarPedido(Guid token);

        Task<IEnumerable<PedidoDTO>> ConsultarPedidos(DateTime fechaIni, DateTime fechaFin);

        Task EliminarPedido(int idPedido);

        #endregion

        #region Evento

        Task<IEnumerable<EventoDTO>> ConsultarEventos();
        Task<ParametersDTO> ConsultarParametros();

        #endregion

    }

}