using Itdear.Aplicacion.Core;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.Extensions.Logging;
using ECM.Aplicacion.DTO.ModuloMob;
using ECM.Aplicacion.Servicios.Interfaz.ModuloMob;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ECM.Dominio.ModuloMob.Entities;
using System.Linq;
using Itdear.Infraestructura.Transversal.Exception;
using Microsoft.EntityFrameworkCore;

namespace ECM.Aplicacion.Servicios.ModuloMob
{
    public class GestionModAppService : BaseAppService, IGestionModAppService
    {
        #region Constructor

        private readonly IUnitOfWorkECM _unitOfWork;
        private readonly IMobCarteraRepository _modCarteraRepositorio;
        private readonly IMobEventosItemsRepository _mobEventosItemsRepositorio;
        private readonly IMobPromocionesRepository _mobPromocionesRepositorio;
        private readonly IMobRcustConsecRepository _mobRcustConsecRepositorio;
        private readonly IMobRcustUserRepository _mobRcustUserRepositorio;
        private readonly IMobCalendarioRepository _mobCalendarioRepositorio;
        private readonly IMobRprodCustRepository _mobRprodCustRepositorio;
        private readonly IMobRproductosRepository _mobRproductosRepositorio;
        private readonly IMobRusuariosRepository _mobRusuariosRepositorio;
        private readonly IMobRsubMenuRepository _mobRsubMenuRepository;
        private readonly IMobRordHeadCustRepository _mobRordHeadCustRepository;
        private readonly IMobRordLineCustRepository _mobRordLineCustRepository;
        private readonly IMobRperfilesRepository _mobRperfilesRepository;

        public GestionModAppService(
           ILoggerFactory loggerFactory,
           IContextAccessor contextAccessor,
           IUnitOfWorkECM unitOfWork,
           IMobCarteraRepository modCarteraRepositorio,
           IMobEventosItemsRepository mobEventosItemsRepositorio,
           IMobPromocionesRepository mobPromocionesRepositorio,
           IMobRcustConsecRepository mobRcustConsecRepositorio,
           IMobRcustUserRepository mobRcustUserRepositorio,
           IMobCalendarioRepository mobCalendarioRepositorio,
           IMobRprodCustRepository mobRprodCustRepositorio,
           IMobRproductosRepository mobRproductosRepositorio,
           IMobRusuariosRepository mobRusuariosRepositorio,
           IMobRsubMenuRepository mobRsubMenuRepository,
           IMobRordHeadCustRepository mobRordHeadCustRepository,
           IMobRordLineCustRepository mobRordLineCustRepository,
           IMobRperfilesRepository mobRperfilesRepository
           ) : base(contextAccessor, loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _modCarteraRepositorio = modCarteraRepositorio;
            _mobEventosItemsRepositorio = mobEventosItemsRepositorio;
            _mobPromocionesRepositorio = mobPromocionesRepositorio;
            _mobRcustConsecRepositorio = mobRcustConsecRepositorio;
            _mobRcustUserRepositorio = mobRcustUserRepositorio;
            _mobCalendarioRepositorio = mobCalendarioRepositorio;
            _mobRprodCustRepositorio = mobRprodCustRepositorio;
            _mobRproductosRepositorio = mobRproductosRepositorio;
            _mobRusuariosRepositorio = mobRusuariosRepositorio;
            _mobRsubMenuRepository = mobRsubMenuRepository;
            _mobRordHeadCustRepository = mobRordHeadCustRepository;
            _mobRordLineCustRepository = mobRordLineCustRepository;
            _mobRperfilesRepository = mobRperfilesRepository;
        }

        #endregion

        public async Task<IEnumerable<MobCarteraDTO>> ConsultarCarteraCliente(string cmpy, string cust, string succli)
        {
            var result = await _modCarteraRepositorio
              .Query(q => q.CarCmpy == cmpy && q.CarCust == cust && q.CarSuccli == succli)
              .SelectAsync();

            return result.ProjectedAsCollection<MobCarteraDTO>();
        }

        public async Task<IEnumerable<MobCarteraDTO>> ConsultarCarteraCliente(string cmpy, string cust, string succli, int mesesCartera)
        {
            var fechaHoy = DateTime.Now.Date;

            fechaHoy = fechaHoy.AddMonths(mesesCartera * -1);

            var result = await _modCarteraRepositorio
              .Query(q => q.CarCmpy == cmpy && q.CarCust == cust && q.CarSuccli == succli && q.CarFechaven >= fechaHoy)
              .SelectAsync();

            return result.ProjectedAsCollection<MobCarteraDTO>();
        }


        public async Task<IEnumerable<MobCalendarioDTO>> ConsultarCalendarioHoy(string cmpy) 
        {
            var fechaHoy = DateTime.Now.Date;

            var result = await _mobCalendarioRepositorio
              .Query(q => q.McaCmpy == cmpy && q.McaFecIni <= fechaHoy && fechaHoy <= q.McaFecFin)
              .SelectAsync();

            return result.ProjectedAsCollection<MobCalendarioDTO>();
        }

        public async Task<IEnumerable<MobEventosItemsDTO>> ConsultarEventosItemsHoy(string cmpy, string type)
        {
            var fechaHoy = DateTime.Now.Date;

            var result = await _mobEventosItemsRepositorio
              .Query(q => q.EiCmpy == cmpy && q.EiType == type && q.EiFecIni <= fechaHoy && fechaHoy <= q.EiFecFin)
              .SelectAsync();

            return result.ProjectedAsCollection<MobEventosItemsDTO>();
        }

        public async Task<IEnumerable<MobEventosItemsDTO>> ConsultarEventosItemsHoy(string cmpy, string type, string id)
        {
            var fechaHoy = DateTime.Now.Date;

            var result = await _mobEventosItemsRepositorio
              .Query(q => q.EiCmpy == cmpy && q.EiType == type && q.EiId == id && q.EiFecIni <= fechaHoy && fechaHoy <= q.EiFecFin)
              .SelectAsync();

            return result.ProjectedAsCollection<MobEventosItemsDTO>();
        }

        public async Task<IEnumerable<MobPromocionesDTO>> ConsultarPromocionesHoy(string cmpy)
        {
            var fechaHoy = DateTime.Now.Date;

            var result = await _mobPromocionesRepositorio
              .Query(q => q.PrmCmpy == cmpy && q.PrmFecIni <= fechaHoy && fechaHoy <= q.PrmFecFin)
              .SelectAsync();

            return result.ProjectedAsCollection<MobPromocionesDTO>();
        }

        public async Task<MobRcustConsecDTO> ConsultarConsecutivoCliente(string cmpy, string cust, string succli, string tipdoc)
        {
            var consecutivo = _mobRcustConsecRepositorio.ConsecutivoMax(cmpy, cust, succli, tipdoc);

            var result = await _mobRcustConsecRepositorio
              .Query(q => q.MdcCmpy == cmpy && q.MdcCust == cust && q.MdcSuccli == succli && q.MdcTipdoc == tipdoc && q.MdcConsec == consecutivo)
              .FirstOrDefaultAsync();

            return result.ProjectedAs<MobRcustConsecDTO>();
        }

        public async Task<MobRcustConsecDTO> CrearConsecutivoCliente(MobRcustConsecDTO item)
        {
            var registro = new MobRcustConsec
            {
                MdcCmpy = item.MdcCmpy,
                MdcCust = item.MdcCust,
                MdcSuccli = item.MdcSuccli,
                MdcTipdoc = item.MdcTipdoc,
                MdcConsec = item.MdcConsec,
                MdcEnter = item.MdcEnter
            };

            registro.MdcEntdt = DateTime.Now;
            registro.MdcTime = DateTime.Now.ToString("HH:mm:ss");

            _mobRcustConsecRepositorio.Insert(registro);

            _unitOfWork.SaveChanges();

            return registro.ProjectedAs<MobRcustConsecDTO>();
        }

        public async Task<MobRcustConsecDTO> ActualizarConsecutivoCliente(string cmpy, string cust, string succli, string tipdoc)
        {
            var registro = await _mobRcustConsecRepositorio.FindAsync(new object[] { cmpy, cust, succli, tipdoc });
            registro.MdcConsec += 1;

            _mobRcustConsecRepositorio.Update(registro);

            await _unitOfWork.SaveChangesAsync();

            return await ConsultarConsecutivoCliente(cmpy, cust, succli, tipdoc);
        }

        public async Task<MobRcustUserDTO> ConsultarClienteUsuario(string cmpy, string cust, string succli)
        {
            var result = await _mobRcustUserRepositorio
              .Query(q => q.MrcuCmpy == cmpy && q.MrcuCust == cust && q.MrcuSuccli == succli)
              .FirstOrDefaultAsync();

            return result.ProjectedAs<MobRcustUserDTO>();
        }


        public async Task<MobRcustUserDTO> ActualizarClienteUsuario(MobRcustUserDTO item)
        {
            var registro = await _mobRcustUserRepositorio.FindAsync(new object[] { item.MrcuCmpy, item.MrcuCust, item.MrcuSuccli });
            registro.MrcuAutorizado = item.MrcuAutorizado;
            registro.MrcuName = item.MrcuName;
            registro.MrcuTele = item.MrcuTele;
            registro.MrcuEmail = item.MrcuEmail;
            registro.MrcuVrMinimo = item.MrcuVrMinimo;
            registro.MrcuVrMaximo = item.MrcuVrMaximo;

            registro.MrcuSchrc = item.MrcuSchrc;
            registro.MrcuIva = item.MrcuIva;
            registro.MrcuType = item.MrcuType;
            registro.MrcuTipCliente = item.MrcuTipCliente;
            registro.MrcuLevel = item.MrcuLevel;
            registro.MrcuDiasEnt = item.MrcuDiasEnt;

            _mobRcustUserRepositorio.Update(registro);

            await _unitOfWork.SaveChangesAsync();

            return await ConsultarClienteUsuario(item.MrcuCmpy, item.MrcuCust, item.MrcuSuccli);
        }
        
        public IEnumerable<MobRlineaDTO> ConsultarLineasCliente(string cmpy, string cust, string succli)
        {
            var result = _mobRprodCustRepositorio
              .Query(q => q.MrpcCmpy == cmpy && q.MrpcCust == cust && q.MrpcSuccli == succli)
              .Include(q => q.MobRproductos.MobRlinea).Select(s => s.MobRproductos.MobRlinea).Distinct();

            return result.ProjectedAsCollection<MobRlineaDTO>();
        }

        public IEnumerable<MobRsubLineasDTO> ConsultarSubLineasCliente(string cmpy, string cust, string succli)
        {
            var result = _mobRprodCustRepositorio
              .Query(q => q.MrpcCmpy == cmpy && q.MrpcCust == cust && q.MrpcSuccli == succli)
              .Include(q => q.MobRproductos.MobRlinea).Select(s => s.MobRproductos.MobRsubLineas).Distinct();

            return result.ProjectedAsCollection<MobRsubLineasDTO>();
        }

        public async Task<MobRperfilesDTO> ConsultarPerfilUsuario(string cmpy, string codper)
        {
            //var usuario = await _mobRusuariosRepositorio
            //   .Query(q => q.RusuCmpy == cmpy && q.RusuCodper == codper)
            //   .Include(i => i.MobRperfiles)
            //   //.Include(i => i.MobRperfiles.MobRperfilMenuPpal)
            //   .FirstOrDefaultAsync();

            //if (usuario == null)
            //{
            //    throw new ValidationException("Usuario no valido");
            //}

            var perfil = await _mobRperfilesRepository.FindAsync(new object[] { cmpy, codper });

            var result = _mobRsubMenuRepository.MenuPerfil(cmpy, codper);

            var subMenu = result.ToList();

            var menuPpal = subMenu.GroupBy(g => g.MobRmenuPpal);

            var perfilDTO = perfil.ProjectedAs<MobRperfilesDTO>();
            perfilDTO.MobRmenuPpal = new List<MobRmenuPpalDTO>();
            
            foreach (var g in menuPpal)
            {
                perfilDTO.MobRmenuPpal.Add(g.Key.ProjectedAs<MobRmenuPpalDTO>());
            }         

            return perfilDTO;
        }

        public async Task<MobRproductosDTO> ConsultarProducto(string cmpy, string id)
        {
            var result = await _mobRproductosRepositorio
             .Query(q => q.RprCmpy == cmpy && q.RprId == id)
             .Include(i => i.MobRlinea)
             .Include(i => i.MobRsubLineas)
             .FirstOrDefaultAsync();

            return result.ProjectedAs<MobRproductosDTO>();
        }

        public async Task<IEnumerable<MobRprodCustDTO>> ConsultarProductosCliente(string cmpy, string cust, string succli)
        {
            var result = await _mobRprodCustRepositorio
              .Query(q => q.MrpcCmpy == cmpy && q.MrpcCust == cust && q.MrpcSuccli == succli)
              .Include(q => q.MobRproductos)
              .Include(q => q.MobRproductos.MobRlinea)
              .Include(q => q.MobRproductos.MobRsubLineas)
              .SelectAsync();

            return result.ProjectedAsCollection<MobRprodCustDTO>();
        }


        #region Ordenes
        public async Task<IEnumerable<MobRordHeadCustDTO>> ConsultarEncabezadosOrdenes(string cmpy, string cust, string ship, int hist)
        {
            var items = await _mobRordHeadCustRepository
                .Queryable().Where(q => q.OrhCmpy == cmpy && q.OrhCust == cust && q.OrhShip == ship)
                .OrderByDescending(o => o.OrhDate)
                .Take(hist)
                .Select(s => s).ToListAsync();
              
            return items.ProjectedAsCollection<MobRordHeadCustDTO>();
        }

        public async Task<MobRordHeadCustDTO> CrearEncabezadoOrden(MobRordHeadCustDTO orden)
        {
            var registro = new MobRordHeadCust
            {
                OrhCmpy = orden.OrhCmpy,
                OrhNum = orden.OrhNum,
                OrhCust = orden.OrhCust,
                OrhShip = orden.OrhShip,
                OrhEntdt = orden.OrhEntdt,
                OrhTime = orden.OrhTime,
                OrhCom = orden.OrhCom,
                OrhShpd = orden.OrhShpd,
                OrhPo = orden.OrhPo,

                OrhTaxp = orden.OrhTaxp,
                OrhSchrg = orden.OrhSchrg,
                OrhToken = orden.OrhToken,

                OrhNum2 = orden.OrhNum2,
                OrhMarca = orden.OrhMarca,
                OrhDate = orden.OrhDate,
            };
            //registro.OrhDate = DateTime.Now;
            _mobRordHeadCustRepository.Insert(registro);

            _unitOfWork.SaveChanges();

            return registro.ProjectedAs<MobRordHeadCustDTO>();
        }

        public async Task<MobRordHeadCustDTO> ActualizarEncabezadoOrden(MobRordHeadCustDTO orden)
        {
            var registro = await _mobRordHeadCustRepository.FindAsync(new object[] { orden.OrhNum, orden.OrhCust, orden.OrhShip });

            if (registro == null)
            {
                throw new BadRequestCustomException("Error al actualizar el registro", "No se encontró el registro que desea actualizar");
            }

            registro.OrhCmpy = orden.OrhCmpy;
            registro.OrhEntdt = orden.OrhEntdt;
            registro.OrhTime = orden.OrhTime;
            registro.OrhCom = orden.OrhCom;
            registro.OrhShpd = orden.OrhShpd;
            registro.OrhPo = orden.OrhPo;

            registro.OrhTaxp = orden.OrhTaxp;
            registro.OrhSchrg = orden.OrhSchrg;
            registro.OrhToken = orden.OrhToken;

            registro.OrhNum2 = orden.OrhNum2;
            registro.OrhMarca = orden.OrhMarca;
            registro.OrhDate = orden.OrhDate;
            //registro.OrhDate = DateTime.Now;

            _mobRordHeadCustRepository.Update(registro);

            await _unitOfWork.SaveChangesAsync();

            return registro.ProjectedAs<MobRordHeadCustDTO>();
        }

        public async Task EliminarEncabezadoOrden(MobRordHeadCustDTO orden)
        {
            var registroEliminado = await _mobRordHeadCustRepository.DeleteAsync(new object[] { orden.OrhNum, orden.OrhCust, orden.OrhShip });

            if (!registroEliminado)
            {
                throw new BadRequestCustomException("Error al eliminar el registro", "No se encontró el registro que desea eliminar...");
            }
            await _unitOfWork.SaveChangesAsync();           
        }

        #endregion

        #region Orden Linea

        public async Task<IEnumerable<MobRordLineCustDTO>> ConsultarDetalleOrden(int num, string cust, string ship) 
        {
            var registros = await _mobRordLineCustRepository.Query(q => q.OrlNum == num && q.OrlCust == cust &&  q.OrlShip == ship).SelectAsync();
                        
            return registros.ProjectedAsCollection<MobRordLineCustDTO>();
        }

        public async Task<MobRordLineCustDTO> CrearDetalleOrden(MobRordLineCustDTO linea)
        {
            var registro = new MobRordLineCust
            {
                OrlCmpy = linea.OrlCmpy,
                OrlNum = linea.OrlNum,
                OrlCust = linea.OrlCust,
                OrlShip = linea.OrlShip,
                OrlPart = linea.OrlPart,
                OrlEntdt = linea.OrlEntdt,
                OrlTime = linea.OrlTime,
                OrlQord = linea.OrlQord,
                OrlTxbl = linea.OrlTxbl,
                OrlLevel = linea.OrlLevel,
                OrlUnpr = linea.OrlUnpr,
                OrlMarca = linea.OrlMarca,
                OrlDate = linea.OrlDate,
            };

            //registro.OrlDate = DateTime.Now;
            _mobRordLineCustRepository.Insert(registro);

            _unitOfWork.SaveChanges();

            return registro.ProjectedAs<MobRordLineCustDTO>();
        }

        public async Task CrearDetallesOrden(IEnumerable<MobRordLineCustDTO> lineas) 
        {
            foreach (var linea in lineas)
            {
                var registro = new MobRordLineCust
                {
                    OrlCmpy = linea.OrlCmpy,
                    OrlNum = linea.OrlNum,
                    OrlCust = linea.OrlCust,
                    OrlShip = linea.OrlShip,
                    OrlPart = linea.OrlPart,
                    OrlEntdt = linea.OrlEntdt,
                    OrlTime = linea.OrlTime,
                    OrlQord = linea.OrlQord,
                    OrlTxbl = linea.OrlTxbl,
                    OrlLevel = linea.OrlLevel,
                    OrlUnpr = linea.OrlUnpr,
                    OrlMarca = linea.OrlMarca,
                    OrlDate = linea.OrlDate,
                };
                _mobRordLineCustRepository.Insert(registro);
            }

            _unitOfWork.SaveChanges();

        }

        public async Task<MobRordLineCustDTO> ActualizarDetalleOrden(MobRordLineCustDTO linea)
        {
            var registro = await _mobRordLineCustRepository.FindAsync(new object[] { linea.OrlNum, linea.OrlCust, linea.OrlShip, linea.OrlPart });
            
            if (registro == null)
            {
                throw new BadRequestCustomException("Error al actualizar el registro", "No se encontró el registro que desea actualizar");
            }

            registro.OrlCmpy = linea.OrlCmpy;
            registro.OrlEntdt = linea.OrlEntdt;
            registro.OrlTime = linea.OrlTime;
            registro.OrlQord = linea.OrlQord;
            registro.OrlTxbl = linea.OrlTxbl;
            registro.OrlLevel = linea.OrlLevel;
            registro.OrlUnpr = linea.OrlUnpr;
            registro.OrlMarca = linea.OrlMarca;
            registro.OrlDate = linea.OrlDate;
            //registro.OrlDate = DateTime.Now;

            _mobRordLineCustRepository.Update(registro);

            await _unitOfWork.SaveChangesAsync();

            return registro.ProjectedAs<MobRordLineCustDTO>();
        }


        public async Task ActualizarDetallesOrden(IEnumerable<MobRordLineCustDTO> lineas)
        {

            foreach (var linea in lineas)
            {
                var registro = await _mobRordLineCustRepository.FindAsync(new object[] { linea.OrlNum, linea.OrlCust, linea.OrlShip, linea.OrlPart });

                if (registro == null)
                {
                    throw new BadRequestCustomException("Error al actualizar el registro", $"No se encontró el registro que desea actualizar con id '{linea.OrlPart}'");
                }

                registro.OrlCmpy = linea.OrlCmpy;
                registro.OrlEntdt = linea.OrlEntdt;
                registro.OrlTime = linea.OrlTime;
                registro.OrlQord = linea.OrlQord;
                registro.OrlTxbl = linea.OrlTxbl;
                registro.OrlLevel = linea.OrlLevel;
                registro.OrlUnpr = linea.OrlUnpr;
                registro.OrlMarca = linea.OrlMarca;
                registro.OrlDate = linea.OrlDate;
                //registro.OrlDate = DateTime.Now;

                _mobRordLineCustRepository.Update(registro);

            }
            await _unitOfWork.SaveChangesAsync();            
        }

        public async Task EliminarDetalleOrden(MobRordLineCustDTO linea)
        {
            var registroEliminado = await _mobRordLineCustRepository.DeleteAsync(new object[] { linea.OrlNum, linea.OrlCust, linea.OrlShip, linea.OrlPart });

            if (!registroEliminado)
            {
                throw new BadRequestCustomException("Error al eliminar el registro", "No se encontró el registro que desea eliminar...");
            }
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
