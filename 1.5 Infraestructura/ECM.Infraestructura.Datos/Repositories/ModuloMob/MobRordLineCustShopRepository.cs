using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRordLineCustShopRepository : Repository<MobRordLineCustShop>, IMobRordLineCustShopRepository
    {
        public MobRordLineCustShopRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }

        public IQueryable<MobRordLineCustShop> ConsultarCarrito(string cmpy, string cust, string succli)
        {

            var setMobRordLineCustShop = _unitOfWork.Set<MobRordLineCustShop>();
            var setMobRprodCust = _unitOfWork.Set<MobRprodCust>();
            var setMobProductos = _unitOfWork.Set<MobRproductos>();

            var query = setMobRordLineCustShop.AsQueryable();

            var queryResult = query.Where(q => q.OrlCmpy == cmpy && q.OrlCust == cust && q.OrlShip == succli)
                .Join(
                setMobRprodCust,
                    pm => new { cmpy = pm.OrlCmpy, cust = pm.OrlCust, succli = pm.OrlShip, part = pm.OrlPart },
                    sm => new { cmpy = sm.MrpcCmpy, cust = sm.MrpcCust, succli = sm.MrpcSuccli, part = sm.MrpcId },
                    (c, s) => new { c, s })
                .Select(s => new MobRordLineCustShop()
                {
                    OrlPart = s.c.OrlPart,
                    OrlEntdt = s.c.OrlEntdt,
                    OrlTime = s.c.OrlTime,
                    OrlQord = s.c.OrlQord,
                    OrlTxbl = s.c.OrlTxbl,
                    OrlLevel = s.c.OrlLevel,
                    OrlUnpr = s.c.OrlUnpr,
                    OrlMarca = s.c.OrlMarca,
                    OrlDate = s.c.OrlDate,
                    MobRprodCust = new MobRprodCust()
                    {
                        MrpcPrecio = s.s.MrpcPrecio,
                        MrpcTax = s.s.MrpcTax,
                        MrpcCantMax = s.s.MrpcCantMax,
                        MrpcEan = s.s.MrpcEan,
                        MrpcUnidemPaq = s.s.MrpcUnidemPaq,
                        MobRproductos = new MobRproductos()
                        {
                            RprId = s.s.MobRproductos.RprId,
                            RprDesc = s.s.MobRproductos.RprDesc,
                            RprLinea = s.s.MobRproductos.RprLinea,
                            RprSubl = s.s.MobRproductos.RprSubl,
                            RprNuevo = s.s.MobRproductos.RprNuevo,
                            RprComponentes = s.s.MobRproductos.RprComponentes,
                            RprAplicacion = s.s.MobRproductos.RprAplicacion,
                            RprMediosPubli = s.s.MobRproductos.RprMediosPubli,
                            RprUrlVideoClip = s.s.MobRproductos.RprUrlVideoClip,
                            RprUrlImagen = s.s.MobRproductos.RprUrlImagen,
                            RprEan = s.s.MobRproductos.RprEan,
                            MobRlinea = new MobRlinea()
                            {
                                RlnLinea = s.s.MobRproductos.MobRlinea.RlnLinea,
                                RlnDescr = s.s.MobRproductos.MobRlinea.RlnDescr,
                            },
                            MobRsubLineas = new MobRsubLineas()
                            {
                                RsbLinea = s.s.MobRproductos.MobRsubLineas.RsbLinea,
                                RsbSubl = s.s.MobRproductos.MobRsubLineas.RsbSubl,
                                RsbDescr = s.s.MobRproductos.MobRsubLineas.RsbDescr,
                            },
                        }
                    }
                }
                );
                //.Include("MobRprodCust.MobRproductos");               
                
              
            return queryResult;
        }
    }
}
