using ECM.Aplicacion.DTO.Ecommerce;
using ECM.Aplicacion.DTO.Messages;
using ECM.Aplicacion.Servicios.Interfaz.Messages;
using ECM.Aplicacion.Servicios.Interfaz.ModuloEcommerce;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Aplicacion.Core;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Itdear.Infraestructura.Transversal.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECM.Aplicacion.Servicios.ModuloEcommerce
{
    public class ModuloEcommerceAppService : IModuloEcommerceAppService
    {
        private readonly IUnitOfWorkECM _unitOfWorkECM;
        private readonly IContextAccessor _contextAccessor;
        private readonly ILogger _logger;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IMobRprodCustRepository _mobRprodCustRepositorio;
        private readonly IMobPromocionesRepository _mobPromocionesRepositorio;
        private readonly IMobRordLineCustShopRepository _mobRordLineCustShopRepository;
        private readonly IMobRordHeadCustRepository _mobRordHeadCustRepositorio;
        private readonly IMobRordLineCustRepository _mobRordLineCustRepositorio;
        private readonly IMobRcustConsecRepository _mobRcustConsecRepositorio;
        private readonly IMobRcustUserRepository _mobRcustUserRepositorio;
        private readonly IMobCalendarioRepository _mobCalendarioRepositorio;
        private readonly IEcmMempresaRepository _ecmMempresaRepository;
        private readonly IEcmMusuarioRepository _ecmMusuarioRepository;
        private readonly IMobEventosItemsRepository _mobEventosItemsRepository;
        private readonly IConfiguration _configuration;
        private readonly IMobRcustUserRepository _mobRcustUserRepository;

        public ModuloEcommerceAppService(
            IUnitOfWorkECM unitOfWorkECM,
            IContextAccessor contextAccessor,
            ILogger<ModuloEcommerceAppService> logger,
            IMailService mailService,
            ISmsService smsService,
            IMobRprodCustRepository mobRprodCustRepositorio,
            IMobPromocionesRepository mobPromocionesRepositorio,
            IMobRordLineCustShopRepository mobRordLineCustShopRepository,
            IMobRordHeadCustRepository mobRordHeadCustRepositorio,
            IMobRordLineCustRepository mobRordLineCustRepositorio,
            IMobRcustConsecRepository mobRcustConsecRepositorio,
            IMobRcustUserRepository mobRcustUserRepositorio,
            IMobCalendarioRepository mobCalendarioRepositorio,
            IEcmMempresaRepository ecmMempresaRepository,
            IEcmMusuarioRepository ecmMusuarioRepository,
            IMobEventosItemsRepository mobEventosItemsRepository,
            IConfiguration configuration,
            IMobRcustUserRepository mobRcustUserRepository
           )
        {
            _unitOfWorkECM = unitOfWorkECM;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mailService = mailService;
            _smsService = smsService;
            _configuration = configuration;
            _mobRprodCustRepositorio = mobRprodCustRepositorio;
            _mobPromocionesRepositorio = mobPromocionesRepositorio;
            _mobRordLineCustShopRepository = mobRordLineCustShopRepository;
            _mobRordHeadCustRepositorio = mobRordHeadCustRepositorio;
            _mobRordLineCustRepositorio = mobRordLineCustRepositorio;
            _mobRcustConsecRepositorio = mobRcustConsecRepositorio;
            _mobRcustUserRepositorio = mobRcustUserRepositorio;
            _mobCalendarioRepositorio = mobCalendarioRepositorio;
            _ecmMempresaRepository = ecmMempresaRepository;
            _ecmMusuarioRepository = ecmMusuarioRepository;
            _mobEventosItemsRepository = mobEventosItemsRepository;
            _mobRcustUserRepository = mobRcustUserRepository;
        }



        public async Task<IEnumerable<LineaDTO>> ConsultarLineasCliente()
        {
            var subLineas = await _mobRprodCustRepositorio.Queryable()
               .Where(q => q.MrpcCmpy == _contextAccessor.CompanyId && q.MrpcCust == _contextAccessor.UserCust && q.MrpcSuccli == _contextAccessor.UserSuccli)
               .Include(i => i.MobRproductos).ThenInclude(t => t.MobRlinea)
               .Include(i => i.MobRproductos).ThenInclude(t => t.MobRsubLineas)
               .Select(item => new SubLineaDTO()
               {
                   IdLinea = item.MobRproductos.RprLinea,
                   DescLinea = item.MobRproductos.MobRlinea.RlnDescr,
                   IdSubLinea = item.MobRproductos.RprSubl,
                   DescSubLinea = item.MobRproductos.MobRsubLineas.RsbDescr,
               }).Distinct().ToListAsync();

            var groupLineas = subLineas.GroupBy(g => new { g.IdLinea, g.DescLinea });

            List<LineaDTO> lineas = new List<LineaDTO>();

            foreach (var item in groupLineas)
            {
                var linea = new LineaDTO
                {
                    IdLinea = item.Key.IdLinea,
                    DescLinea = item.Key.DescLinea,
                    SubLineas = new List<SubLineaDTO>()
                };
                linea.SubLineas.AddRange(item.ToList());

                lineas.Add(linea);
            }
            return lineas;
        }

        public async Task<IEnumerable<ProductoDTO>> ConsultarProductosCliente()
        {

            return await _mobRprodCustRepositorio.Queryable()
                .Where(q => q.MrpcCmpy == _contextAccessor.CompanyId && q.MrpcCust == _contextAccessor.UserCust && q.MrpcSuccli == _contextAccessor.UserSuccli)
                .Include(i => i.MobRproductos).ThenInclude(t => t.MobRlinea)
                .Include(i => i.MobRproductos).ThenInclude(t => t.MobRsubLineas)
                .Select(item => new ProductoDTO()
                {
                    IdProducto = item.MrpcId,
                    DescProducto = item.MobRproductos.RprDesc.Trim(),
                    IdLinea = item.MobRproductos.RprLinea,
                    DescLinea = item.MobRproductos.MobRlinea.RlnDescr.Trim(),
                    IdSubLinea = item.MobRproductos.RprSubl,
                    DescSubLinea = item.MobRproductos.MobRsubLineas.RsbDescr.Trim(),
                    Nuevo = item.MobRproductos.RprNuevo,
                    UrlImagen = item.MobRproductos.RprUrlImagen,
                    Ean = item.MobRproductos.RprEan,
                    Precio = item.MrpcPrecio,
                    Impuesto = item.MrpcTax,
                    CantidadMaxima = item.MrpcCantMax,
                    UnidadesEmpaque = item.MrpcUnidemPaq
                }).ToListAsync();

        }

        public async Task<IEnumerable<PromocionDTO>> ConsultarPromociones()
        {

            var fechaHoy = DateTime.Now.Date;

            var result = await _mobPromocionesRepositorio
               .Query(q => q.PrmCmpy == _contextAccessor.CompanyId && q.PrmFecIni <= fechaHoy && fechaHoy <= q.PrmFecFin)
               .SelectAsync();

            return result.ProjectedAsCollection<PromocionDTO>();
        }

        #region Carrito

        public async Task ActualizarProductoCarro(ProductoDTO producto)
        {

            producto.CantidadSolicitada = producto._CantidadSolicitada;
            var item = await _mobRordLineCustShopRepository
                .Query(q => q.OrlCmpy == _contextAccessor.CompanyId && q.OrlCust == _contextAccessor.UserCust && q.OrlShip == _contextAccessor.UserSuccli && q.OrlPart == producto.IdProducto)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                MobRordLineCustShop newItem = new MobRordLineCustShop()
                {
                    OrlCmpy = _contextAccessor.CompanyId,
                    OrlNum = 0,
                    OrlCust = _contextAccessor.UserCust,
                    OrlShip = _contextAccessor.UserSuccli,
                    OrlPart = producto.IdProducto,
                    OrlQord = producto.CantidadSolicitada,
                    OrlEntdt = DateTime.Now.Date,
                    OrlTime = DateTime.Now.TimeOfDay.ToString(),
                };

                _mobRordLineCustShopRepository.Insert(newItem);
            }
            else
            {
                if (producto.CantidadSolicitada <= 0)
                {
                    _mobRordLineCustShopRepository.Delete(item);
                }
                else
                {
                    item.OrlQord = producto.CantidadSolicitada;
                    item.OrlEntdt = DateTime.Now.Date;
                    item.OrlTime = DateTime.Now.TimeOfDay.ToString();
                }
            }

            _unitOfWorkECM.SaveChanges();
        }

        public async Task<IEnumerable<ProductoDTO>> ConsultarProductosCarro()
        {

            var items = await _mobRordLineCustShopRepository.ConsultarCarrito(_contextAccessor.CompanyId, _contextAccessor.UserCust, _contextAccessor.UserSuccli).ToListAsync();

            return items.ProjectedAsCollection<ProductoDTO>();

        }

        #endregion


        #region Pedido

        public async Task<int> CrearPedido(PedidoDTO registro)
        {
            registro.PedidoDetalle = registro.PedidoDetalle.Where(s => s.Cantidad > 0);

            if (registro.PedidoDetalle.Count() == 0)
            {
                throw new ValidationException("No se han asociados productos");
            }

            var fechaHoy = DateTime.Now.Date;
            var calendar = await _mobCalendarioRepositorio.Query(q => q.McaCmpy == _contextAccessor.CompanyId && q.McaFecIni <= fechaHoy && fechaHoy <= q.McaFecFin).FirstOrDefaultAsync();

            if (calendar == null)
            {
                throw new ValidationException("Se encuentra deshabilitada la opción de creación de pedidos");
            }

            var cliente = await _mobRcustUserRepositorio
                .Query(q => q.MrcuCmpy == _contextAccessor.CompanyId && q.MrcuCust == _contextAccessor.UserCust && q.MrcuSuccli == _contextAccessor.UserSuccli)
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new NotFoundCustomException("Cliente", "No encontrado");
            }

            if (cliente.MrcuIva.GetValueOrDefault() != registro.PorcIva)
            {
                throw new ValidationException("El porcentaje de IVA asociado al cliente es diferente al asignado");
            }

            if (cliente.MrcuSchrc.GetValueOrDefault() != registro.PorcDescuento)
            {
                throw new ValidationException("El porcentaje de descuento asociado al cliente es diferente al asignado");
            }

            var consecutivoCliente = await _mobRcustConsecRepositorio.Queryable().Where(q => q.MdcCmpy == _contextAccessor.CompanyId
                    && q.MdcCust == _contextAccessor.UserCust
                    && q.MdcSuccli == _contextAccessor.UserSuccli
                    && q.MdcTipdoc == "P").MaxAsync(m => m.MdcConsec) + 1;

            MobRcustConsec nuevoRegistro = new MobRcustConsec()
            {
                MdcCmpy = _contextAccessor.CompanyId,
                MdcCust = _contextAccessor.UserCust,
                MdcSuccli = _contextAccessor.UserSuccli,
                MdcTipdoc = "P",
                //MdcConsec = consecutivo,
                MdcConsec = consecutivoCliente,
                MdcEnter = "",
                MdcEntdt = DateTime.Now.Date,
                MdcTime = DateTime.Now.TimeOfDay.ToString(),
            };
            _mobRcustConsecRepositorio.Insert(nuevoRegistro);

            MobRordHeadCust pedido = new MobRordHeadCust()
            {
                OrhCmpy = _contextAccessor.CompanyId,
                //OrhNum = consecutivo,
                OrhNum = consecutivoCliente,
                OrhCust = _contextAccessor.UserCust,
                OrhShip = _contextAccessor.UserSuccli,
                OrhEntdt = DateTime.Now.Date,
                OrhTime = DateTime.Now.TimeOfDay.ToString(),
                OrhMarca = "N",
                OrhSchrg = cliente.MrcuSchrc.GetValueOrDefault(),
                OrhTaxp = cliente.MrcuIva.GetValueOrDefault(),
                OrhCom = registro.Observaciones,
                OrhToken = Guid.NewGuid().ToString(),
                MobRordLineCust = new List<MobRordLineCust>()
            };

            string msgValidacionProductos = "";

            foreach (var item in registro.PedidoDetalle)
            {
                var productoCliente = await _mobRprodCustRepositorio
                    .Query(q => q.MrpcCmpy == _contextAccessor.CompanyId && q.MrpcCust == _contextAccessor.UserCust && q.MrpcSuccli == _contextAccessor.UserSuccli && q.MrpcId == item.Producto.IdProducto)
                    .FirstOrDefaultAsync();

                if (productoCliente == null)
                {
                    msgValidacionProductos += $"No se encuentra el producto '{item.Producto.DescProducto}' con código {item.Producto.IdProducto} asociado al cliente. /n";
                }
                else
                {
                    if (productoCliente.MrpcCantMax < item.Cantidad)
                    {
                        msgValidacionProductos += $"La cantidad máxima para el producto '{item.Producto.DescProducto}' con código {item.Producto.IdProducto} es {productoCliente.MrpcCantMax}. /n";
                    }

                    if (item.Cantidad % productoCliente.MrpcUnidemPaq != 0)
                    {
                        msgValidacionProductos += $"La unidad de empaque para el producto '{item.Producto.DescProducto}' con código {item.Producto.IdProducto} es de '{productoCliente.MrpcUnidemPaq}', la cantidad solicitada debe ser múltiplo de las unidades de empaque. /n";
                    }

                    if ((productoCliente.MrpcTax.ToLower().Trim() == "y") != item.AplicaIva)
                    {
                        msgValidacionProductos += $"Si aplica iva para el producto '{item.Producto.DescProducto}' con código {item.Producto.IdProducto} es diferente al asignado para el cliente. /n";
                    }
                }

                MobRordLineCust detalle = new MobRordLineCust()
                {
                    OrlCmpy = _contextAccessor.CompanyId,
                    OrlPart = item.Producto.IdProducto,
                    OrlEntdt = DateTime.Now.Date,
                    OrlTime = DateTime.Now.TimeOfDay.ToString(),
                    OrlQord = item.Cantidad,
                    OrlTxbl = productoCliente.MrpcTax.ToUpper().Trim(),
                    OrlLevel = cliente.MrcuLevel,
                    OrlUnpr = productoCliente.MrpcPrecio,
                    OrlMarca = "N"
                };

                pedido.MobRordLineCust.Add(detalle);
            }

            decimal subTotal = 0;
            decimal descuento = 0;
            decimal totalAntesIva = 0;
            decimal iva = 0;
            decimal total = 0;
            foreach (var item in pedido.MobRordLineCust)
            {
                var subTotalAux = item.OrlQord.GetValueOrDefault() * item.OrlUnpr.GetValueOrDefault();
                subTotal += subTotalAux;

                var descuentoAux = subTotalAux * pedido.OrhSchrg.GetValueOrDefault() / 100;
                descuento += descuentoAux;

                var totalAntesIvaAux = subTotalAux - descuentoAux;
                totalAntesIva += totalAntesIvaAux;

                var ivaAux = item.OrlTxbl == "Y" ? (totalAntesIvaAux * pedido.OrhTaxp.GetValueOrDefault() / 100) : 0;
                iva += ivaAux;
                total += totalAntesIvaAux + ivaAux;
            }

            if (Math.Abs(registro.TotalAux - total) > 1)
            {
                msgValidacionProductos += $"El valor total del pedido es diferente al calculado '{registro.TotalAux:n0}'. /n";
            }

            if (cliente.MrcuVrMinimo > totalAntesIva)
            {
                // msgValidacionProductos += $"El valor mínimo del pedido antes de IVA debe ser de '{cliente.MrcuVrMinimo.GetValueOrDefault():n0}'. /n";
                msgValidacionProductos += $"El valor mínimo del pedido debe ser de '{cliente.MrcuVrMinimo.GetValueOrDefault():n0}'. /n";
            }

            if (cliente.MrcuVrMaximo < total)
            {
                // msgValidacionProductos += $"El valor máximo del pedido IVA incluido debe ser de '{cliente.MrcuVrMaximo.GetValueOrDefault():n0}'. /n";
                msgValidacionProductos += $"El valor máximo del pedido debe ser de '{cliente.MrcuVrMaximo.GetValueOrDefault():n0}'. /n";
            }

            if (!string.IsNullOrWhiteSpace(msgValidacionProductos))
            {
                throw new ValidationException(msgValidacionProductos);
            }

            _mobRordHeadCustRepositorio.Insert(pedido);

            var itemsCarrito = await _mobRordLineCustShopRepository
                .Query(q => q.OrlCmpy == _contextAccessor.CompanyId && q.OrlCust == _contextAccessor.UserCust && q.OrlShip == _contextAccessor.UserSuccli)
                .SelectAsync();

            foreach (var itemCarrito in itemsCarrito)
            {
                _mobRordLineCustShopRepository.Delete(itemCarrito);
            }

            _unitOfWorkECM.SaveChanges();

            // Envio de correo y sms pedido generado
            await EnviarMensajePedidoToken(pedido.OrhNum, pedido.OrhToken, pedido.OrhEntdt.GetValueOrDefault(), subTotal, descuento, iva, total);

            return consecutivoCliente;
        }

        private async Task EnviarMensajePedidoToken(int idPedido, string token, DateTime fechaPedido, decimal valorParcial, decimal valorDescuento, decimal valorIva, decimal valorTotal) {
            var parametersMsg = _configuration.GetSection("OrderTokenMsg").Get<ParametersMsgDTO>();

            var usuario = await _ecmMusuarioRepository.Query(q => q.MusuaCust == _contextAccessor.UserCust && q.MusuaSuccli == _contextAccessor.UserSuccli).FirstOrDefaultAsync();

            var empresa = await _ecmMempresaRepository
                                    .Query(q => q.MemprCmpy == _contextAccessor.CompanyId)
                                    .FirstOrDefaultAsync();

            var telefono = $"{empresa.MemprePaisId}{usuario.MusuaPhone}";

            var msgSms = parametersMsg.TextSms.Replace("{idEmpresa}", _contextAccessor.CompanyId).Replace("{token}", token).Replace("{idPedido}", idPedido.ToString());

            await _smsService.Send(empresa.MemprApisms, parametersMsg.FromSms, telefono, msgSms);

            var url = parametersMsg.Url.Replace("{idEmpresa}", _contextAccessor.CompanyId).Replace("{token}", token);

            var parametros = new
            {
                Usuario = usuario.MusuaName,
                NumeroPedido = idPedido,
                linkpedido = url,
                FechaPedido = fechaPedido.ToString(empresa.MemprFdate),
                VrParcial = $"{empresa.MemprFcurr} {valorParcial:n0}",
                VrDcto = $"{empresa.MemprFcurr} {valorDescuento:n0}",
                VrIva = $"{empresa.MemprFcurr} {valorIva:n0}",
                VrTotal = $"{empresa.MemprFcurr} {valorTotal:n0}"
            };

            var subject = parametersMsg.Subject.Replace("{idPedido}", idPedido.ToString());
            await _mailService.Send(empresa.MemprApisms, parametersMsg.FromMail, usuario.MusuaMail, subject, parametersMsg.TemplateId, parametros, generateException: true);

        }

        public async Task<PedidoDTO> ActualizarPedido(PedidoDTO registro)
        {
            var pedido = await _mobRordHeadCustRepositorio.Query(q => q.OrhCmpy == _contextAccessor.CompanyId && q.OrhCust == _contextAccessor.UserCust
            && q.OrhShip == _contextAccessor.UserSuccli && q.OrhNum == registro.IdPedido).FirstOrDefaultAsync();

            if (pedido == null) {
                throw new ValidationException($"No existe el pedido número {registro.IdPedido}");
            }

            return await ConsultarPedido(registro.IdPedido);
        }

        public async Task<PedidoDTO> ConsultarPedido(int idPeddo)
        {           

            var pedido = await _mobRordHeadCustRepositorio.Queryable()
                .Where(q => q.OrhCmpy == _contextAccessor.CompanyId && q.OrhCust == _contextAccessor.UserCust && q.OrhShip == _contextAccessor.UserSuccli && q.OrhNum == idPeddo)
                .FirstOrDefaultAsync();

            var result = pedido.ProjectedAs<PedidoDTO>();

            var detallePedido = _mobRordLineCustRepositorio.ConsultarPedido(_contextAccessor.CompanyId, _contextAccessor.UserCust, _contextAccessor.UserSuccli, idPeddo);

            result.PedidoDetalle = detallePedido.ProjectedAsCollection<PedidoDetalleDTO>();

            return result;
        }

        public async Task<PedidoDTO> ConsultarPedido(Guid token)
        {

            var pedido = await _mobRordHeadCustRepositorio.Queryable()
                .Where(q => q.OrhToken == token.ToString())
                .FirstOrDefaultAsync();

            if (pedido == null) {
                throw new BadRequestCustomException("Error", "No existe el pedido");
            }

            var empresa = await _ecmMempresaRepository
                                  .Query(q => q.MemprCmpy == pedido.OrhCmpy)
                                  .FirstOrDefaultAsync();

            var result = pedido.ProjectedAs<PedidoDTO>();

            result.FormatCurrency = empresa.MemprFcurr;
            result.FormatDate = empresa.MemprFdate;
            result.FormatNumber = empresa.MemprFnumb;

            var detallePedido = _mobRordLineCustRepositorio.ConsultarPedido(pedido.OrhCmpy, pedido.OrhCust, pedido.OrhShip, pedido.OrhNum);

            result.PedidoDetalle = detallePedido.ProjectedAsCollection<PedidoDetalleDTO>();

            return result;
        }


        public async Task<IEnumerable<PedidoDTO>> ConsultarPedidos(DateTime fechaIni, DateTime fechaFin)
        {
            var pedidos = await _mobRordHeadCustRepositorio.Queryable()
               .Where(q => q.OrhCmpy == _contextAccessor.CompanyId && q.OrhCust == _contextAccessor.UserCust && q.OrhShip == _contextAccessor.UserSuccli && q.OrhEntdt >= fechaIni.Date && q.OrhEntdt <= fechaFin.Date.AddDays(1))
               .ToListAsync();

            var result = pedidos.ProjectedAsCollection<PedidoDTO>();

            foreach (var item in result)
            {
                var detallePedido = _mobRordLineCustRepositorio.ConsultarPedido(_contextAccessor.CompanyId, _contextAccessor.UserCust, _contextAccessor.UserSuccli, item.IdPedido);

                item.PedidoDetalle = detallePedido.ProjectedAsCollection<PedidoDetalleDTO>();
            }
            
            return result.OrderByDescending(o => o.IdPedido);
        }

        public async Task EliminarPedido(int idPedido) {

            var pedido = await _mobRordHeadCustRepositorio.Query(q => q.OrhNum == idPedido && q.OrhCust == _contextAccessor.UserCust && q.OrhShip == _contextAccessor.UserSuccli).FirstOrDefaultAsync();

            if (pedido.OrhMarca != "N") {
                throw new Exception($"El pedido número '{idPedido}' se encuentra en un estado que no permite su eliminación");
            }

            await _mobRordHeadCustRepositorio.DeleteAsync(new object[] { pedido.OrhNum, pedido.OrhCust, pedido.OrhShip  });

            var detallesPedido = await _mobRordLineCustRepositorio.Query(q => q.OrlNum == idPedido && q.OrlCust == _contextAccessor.UserCust && q.OrlShip == _contextAccessor.UserSuccli).SelectAsync();

            foreach (var detalle in detallesPedido)
            {
                if (detalle.OrlMarca != "N")
                {
                    throw new Exception($"El pedido número '{idPedido}' tiene líneas que se encuentra en un estado que no permite su eliminación");
                }
                await _mobRordLineCustRepositorio.DeleteAsync(new object[] { detalle.OrlNum, detalle.OrlCust, detalle.OrlShip, detalle.OrlPart });
            }           

            _unitOfWorkECM.SaveChanges();
        }

        #endregion


        #region Evento

        public async Task<IEnumerable<EventoDTO>> ConsultarEventos() {

            var fechaHoy = DateTime.Now.Date;

            var cliente = await _mobRcustUserRepositorio
               .Query(q => q.MrcuCmpy == _contextAccessor.CompanyId && q.MrcuCust == _contextAccessor.UserCust && q.MrcuSuccli == _contextAccessor.UserSuccli)
               .FirstOrDefaultAsync();

            var setMobRprodCust = _unitOfWorkECM.Set<MobRprodCust>();
            var setMobRproductos = _unitOfWorkECM.Set<MobRproductos>();

            var result = await _mobEventosItemsRepository.Queryable()
               .Where(q => q.EiCmpy == _contextAccessor.CompanyId && q.EiFecIni <= fechaHoy && fechaHoy <= q.EiFecFin && q.EiType == cliente.MrcuType)
               .Join(
                setMobRprodCust,
                    pm => new { cmpy = pm.EiCmpy, cust = _contextAccessor.UserCust, succli = _contextAccessor.UserSuccli, part = pm.EiId },
                    sm => new { cmpy = sm.MrpcCmpy, cust = sm.MrpcCust, succli = sm.MrpcSuccli, part = sm.MrpcId },
                    (c, s) => new { c, s })
               .Join(
                setMobRproductos,
                    pm => new { cmpy = pm.s.MrpcCmpy, part = pm.s.MrpcId },
                    sm => new { cmpy = sm.RprCmpy, part = sm.RprId },
                    (c, s) => new { c, s })
              .Select(s => new EventoDTO()
              {
                  IdTipo = s.c.c.EiType,
                  FechaIni = s.c.c.EiFecIni.GetValueOrDefault(),
                  FechaFin = s.c.c.EiFecFin.GetValueOrDefault(),
                  DescEvento = s.c.c.EiDesc,
                  Producto = new ProductoDTO()
                  {
                      IdProducto = s.s.RprId,
                      DescProducto = s.s.RprDesc,
                      Ean = s.s.RprEan,
                      UrlImagen = s.s.RprUrlImagen,
                      IdLinea = s.s.MobRlinea.RlnLinea,
                      DescLinea = s.s.MobRlinea.RlnDescr,
                      IdSubLinea = s.s.MobRsubLineas.RsbLinea,
                      DescSubLinea = s.s.MobRsubLineas.RsbDescr,
                  }
              }).ToListAsync();

            return result;
        }

        #endregion

        #region Parametros

        public async Task<ParametersDTO> ConsultarParametros()
        {
            var cliente = await _mobRcustUserRepository
                           .Query(q => q.MrcuCmpy == _contextAccessor.CompanyId && q.MrcuCust == _contextAccessor.UserCust && q.MrcuSuccli == _contextAccessor.UserSuccli)
                           .FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new NotFoundCustomException("Cliente", "No encontrado");
            }

            var fechaHoy = DateTime.Now.Date;
            var calendar = await _mobCalendarioRepositorio.Query(q => q.McaCmpy == _contextAccessor.CompanyId && q.McaFecIni <= fechaHoy && fechaHoy <= q.McaFecFin).FirstOrDefaultAsync();


            ParametersDTO parameters = new ParametersDTO()
            {
                MinValueOrder = cliente.MrcuVrMinimo.GetValueOrDefault(),
                MaxValueOrder = cliente.MrcuVrMaximo.GetValueOrDefault(),
                DiscountRate = cliente.MrcuSchrc.GetValueOrDefault(),
                TaxRate = cliente.MrcuIva.GetValueOrDefault(),
                TakeOrders = calendar != null
            };

            return parameters;
        }

        #endregion
    }
}
