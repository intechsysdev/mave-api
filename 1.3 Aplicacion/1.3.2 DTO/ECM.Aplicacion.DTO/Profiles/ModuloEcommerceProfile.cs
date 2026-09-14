using AutoMapper;
using ECM.Aplicacion.DTO.Cliente;
using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Dominio.ModuloMob.Entities;

namespace ECM.Aplicacion.DTO.Profiles
{
    public class ModuloEcommerceProfile : Profile
    {
        public ModuloEcommerceProfile()
        {
            AllowNullDestinationValues = true;

            var promocionesMappingExpression = CreateMap<PromocionDTO, MobPromociones>().ReverseMap();
            promocionesMappingExpression.ForMember(dto => dto.FechaIni, (map) => map.MapFrom(o => o.PrmFecIni));
            promocionesMappingExpression.ForMember(dto => dto.FechaFin, (map) => map.MapFrom(o => o.PrmFecFin));
            promocionesMappingExpression.ForMember(dto => dto.UrlBanner, (map) => map.MapFrom(o => o.PrmUrlbanner));
            promocionesMappingExpression.ForMember(dto => dto.DescPromocion, (map) => map.MapFrom(o => o.PrmBanner));

            //var mobRordLineCustMappingExpression = CreateMap<PedidoDetalleDTO, MobRordLineCustShop>().ReverseMap();
            //mobRordLineCustMappingExpression.ForMember(dto => dto.Cantidad, (map) => map.MapFrom(o => o.OrlQord));
            //mobRordLineCustMappingExpression.ForMember(dto => dto.Producto, (map) => map.MapFrom(o => o.MobRprodCust));

            //var mobRproductosMappingExpression = CreateMap<ProductoDTO, MobRprodCust>().ReverseMap();
            //mobRproductosMappingExpression.ForMember(dto => dto.IdProducto, (map) => map.MapFrom(o => o.MobRproductos.RprId));
            //mobRproductosMappingExpression.ForMember(dto => dto.DescProducto, (map) => map.MapFrom(o => o.MobRproductos.RprDesc));
            //mobRproductosMappingExpression.ForMember(dto => dto.IdLinea, (map) => map.MapFrom(o => o.MobRproductos.RprLinea));
            //mobRproductosMappingExpression.ForMember(dto => dto.DescLinea, (map) => map.MapFrom(o => o.MobRproductos.MobRlinea.RlnDescr));
            //mobRproductosMappingExpression.ForMember(dto => dto.IdSubLinea, (map) => map.MapFrom(o => o.MobRproductos.RprSubl));
            //mobRproductosMappingExpression.ForMember(dto => dto.DescSubLinea, (map) => map.MapFrom(o => o.MobRproductos.MobRsubLineas.RsbDescr));
            //mobRproductosMappingExpression.ForMember(dto => dto.Nuevo, (map) => map.MapFrom(o => o.MobRproductos.RprNuevo));
            //mobRproductosMappingExpression.ForMember(dto => dto.UrlImagen, (map) => map.MapFrom(o => o.MobRproductos.RprUrlImagen));
            //mobRproductosMappingExpression.ForMember(dto => dto.Ean, (map) => map.MapFrom(o => o.MobRproductos.RprEan));
            //mobRproductosMappingExpression.ForMember(dto => dto.Precio, (map) => map.MapFrom(o => o.MrpcPrecio));
            //mobRproductosMappingExpression.ForMember(dto => dto.Impuesto, (map) => map.MapFrom(o => o.MrpcTax));
            //mobRproductosMappingExpression.ForMember(dto => dto.CantidadMaxima, (map) => map.MapFrom(o => o.MrpcCantMax));

            var mobRproductosMappingExpression = CreateMap<ProductoDTO, MobRordLineCustShop>().ReverseMap();
            mobRproductosMappingExpression.ForMember(dto => dto.IdProducto, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprId));
            mobRproductosMappingExpression.ForMember(dto => dto.DescProducto, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprDesc));
            mobRproductosMappingExpression.ForMember(dto => dto.IdLinea, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprLinea));
            mobRproductosMappingExpression.ForMember(dto => dto.DescLinea, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.MobRlinea.RlnDescr));
            mobRproductosMappingExpression.ForMember(dto => dto.IdSubLinea, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprSubl));
            mobRproductosMappingExpression.ForMember(dto => dto.DescSubLinea, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.MobRsubLineas.RsbDescr));
            mobRproductosMappingExpression.ForMember(dto => dto.Nuevo, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprNuevo));
            mobRproductosMappingExpression.ForMember(dto => dto.UrlImagen, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprUrlImagen));
            mobRproductosMappingExpression.ForMember(dto => dto.Ean, (map) => map.MapFrom(o => o.MobRprodCust.MobRproductos.RprEan));
            mobRproductosMappingExpression.ForMember(dto => dto.Precio, (map) => map.MapFrom(o => o.MobRprodCust.MrpcPrecio));
            mobRproductosMappingExpression.ForMember(dto => dto.Impuesto, (map) => map.MapFrom(o => o.MobRprodCust.MrpcTax));
            mobRproductosMappingExpression.ForMember(dto => dto.CantidadMaxima, (map) => map.MapFrom(o => o.MobRprodCust.MrpcCantMax));
            mobRproductosMappingExpression.ForMember(dto => dto.CantidadSolicitada, (map) => map.MapFrom(o => o.OrlQord));
            mobRproductosMappingExpression.ForMember(dto => dto.UnidadesEmpaque, (map) => map.MapFrom(o => o.MobRprodCust.MrpcUnidemPaq));

            var pedidoMappingExpression = CreateMap<PedidoDTO, MobRordHeadCust>().ReverseMap();
            pedidoMappingExpression.ForMember(dto => dto.IdPedido, (map) => map.MapFrom(o => o.OrhNum));
            pedidoMappingExpression.ForMember(dto => dto.Fecha, (map) => map.MapFrom(o => o.OrhEntdt));
            pedidoMappingExpression.ForMember(dto => dto.Marca, (map) => map.MapFrom(o => o.OrhMarca));
            pedidoMappingExpression.ForMember(dto => dto.PorcDescuento, (map) => map.MapFrom(o => o.OrhSchrg));
            pedidoMappingExpression.ForMember(dto => dto.PorcIva, (map) => map.MapFrom(o => o.OrhTaxp));
            pedidoMappingExpression.ForMember(dto => dto.Observaciones, (map) => map.MapFrom(o => o.OrhCom));
            pedidoMappingExpression.ForMember(dto => dto.Cerrado, (map) => map.MapFrom(o => !o.OrhMarca.ToLower().Trim().Equals("n")));

            var pedidoDetalleMappingExpression = CreateMap<PedidoDetalleDTO, MobRordLineCust>().ReverseMap();
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.IdProducto, (map) => map.MapFrom(o => o.MobRproductos.RprId));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.DescProducto, (map) => map.MapFrom(o => o.MobRproductos.RprDesc));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.IdLinea, (map) => map.MapFrom(o => o.MobRproductos.RprLinea));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.DescLinea, (map) => map.MapFrom(o => o.MobRproductos.MobRlinea.RlnDescr));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.IdSubLinea, (map) => map.MapFrom(o => o.MobRproductos.RprSubl));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.DescSubLinea, (map) => map.MapFrom(o => o.MobRproductos.MobRsubLineas.RsbDescr));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.Nuevo, (map) => map.MapFrom(o => o.MobRproductos.RprNuevo));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.UrlImagen, (map) => map.MapFrom(o => o.MobRproductos.RprUrlImagen));
            //pedidoDetalleMappingExpression.ForMember(dto => dto.Producto.Ean, (map) => map.MapFrom(o => o.MobRproductos.RprEan));
            pedidoDetalleMappingExpression.ForMember(dto => dto.Producto, (map) => map.MapFrom(o => o.MobRproductos));
            pedidoDetalleMappingExpression.ForMember(dto => dto.ValorUnitario, (map) => map.MapFrom(o => o.OrlUnpr));
            pedidoDetalleMappingExpression.ForMember(dto => dto.Cantidad, (map) => map.MapFrom(o => o.OrlQord));
            pedidoDetalleMappingExpression.ForMember(dto => dto.AplicaIva, (map) => map.MapFrom(o => o.OrlTxbl.ToLower().Trim() == "y"));

            var productoMappingExpression = CreateMap<ProductoDTO, MobRproductos>().ReverseMap();
            productoMappingExpression.ForMember(dto => dto.IdProducto, (map) => map.MapFrom(o => o.RprId));
            productoMappingExpression.ForMember(dto => dto.DescProducto, (map) => map.MapFrom(o => o.RprDesc));
            productoMappingExpression.ForMember(dto => dto.IdLinea, (map) => map.MapFrom(o => o.RprLinea));
            productoMappingExpression.ForMember(dto => dto.DescLinea, (map) => map.MapFrom(o => o.MobRlinea.RlnDescr));
            productoMappingExpression.ForMember(dto => dto.IdSubLinea, (map) => map.MapFrom(o => o.RprSubl));
            productoMappingExpression.ForMember(dto => dto.DescSubLinea, (map) => map.MapFrom(o => o.MobRsubLineas.RsbDescr));
            productoMappingExpression.ForMember(dto => dto.Nuevo, (map) => map.MapFrom(o => o.RprNuevo));
            productoMappingExpression.ForMember(dto => dto.UrlImagen, (map) => map.MapFrom(o => o.RprUrlImagen));
            productoMappingExpression.ForMember(dto => dto.Ean, (map) => map.MapFrom(o => o.RprEan));


            var menuMappingExpression = CreateMap<MenuDTO, MobRmenuPpal>().ReverseMap();
            menuMappingExpression.ForMember(dto => dto.Id, (map) => map.MapFrom(o => o.RmenCodmen));
            menuMappingExpression.ForMember(dto => dto.Name, (map) => map.MapFrom(o => o.RmenDesmen));
            menuMappingExpression.ForMember(dto => dto.SubName, (map) => map.MapFrom(o => o.RmenDesmen));
            menuMappingExpression.ForMember(dto => dto.Icon, (map) => map.MapFrom(o => o.RmenIcono));
            menuMappingExpression.ForMember(dto => dto.Url, (map) => map.MapFrom(o => o.RmenAplicacion));
            menuMappingExpression.ForMember(dto => dto.Options, (map) => map.MapFrom(o => o.MobRsubMenu));
            
            var subMenuMappingExpression = CreateMap<MenuDTO, MobRsubMenu>().ReverseMap();
            subMenuMappingExpression.ForMember(dto => dto.Id, (map) => map.MapFrom(o => o.RsubmCodsubmen));
            subMenuMappingExpression.ForMember(dto => dto.Name, (map) => map.MapFrom(o => o.RsubmDessubmen));
            subMenuMappingExpression.ForMember(dto => dto.SubName, (map) => map.MapFrom(o => o.RsubmDessubmen));
            subMenuMappingExpression.ForMember(dto => dto.Icon, (map) => map.MapFrom(o => o.RsubmIcono.Trim()));
            subMenuMappingExpression.ForMember(dto => dto.Url, (map) => map.MapFrom(o => o.RsubmAplicacion.Trim()));
        }
    }
}
