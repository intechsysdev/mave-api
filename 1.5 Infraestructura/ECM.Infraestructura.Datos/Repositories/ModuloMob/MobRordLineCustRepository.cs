using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Linq;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRordLineCustRepository : Repository<MobRordLineCust>, IMobRordLineCustRepository
    {
        public MobRordLineCustRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }

        public IQueryable<MobRordLineCust> ConsultarPedido(string cmpy, string cust, string succli, int num)
        {

            var setMobRordLineCust = _unitOfWork.Set<MobRordLineCust>();
            var setMobProductos = _unitOfWork.Set<MobRproductos>();

            var query = setMobRordLineCust.AsQueryable();

            var queryResult = query.Where(q => q.OrlCmpy == cmpy && q.OrlCust == cust && q.OrlShip == succli && q.OrlNum == num)
                .Select(s => new MobRordLineCust()
                {
                    OrlPart = s.OrlPart,
                    OrlEntdt = s.OrlEntdt,
                    OrlTime = s.OrlTime,
                    OrlQord = s.OrlQord,
                    OrlTxbl = s.OrlTxbl,
                    OrlLevel = s.OrlLevel,
                    OrlUnpr = s.OrlUnpr,
                    OrlMarca = s.OrlMarca,
                    OrlDate = s.OrlDate,
                    MobRproductos = new MobRproductos()
                    {
                        RprId = s.MobRproductos.RprId,
                        RprDesc = s.MobRproductos.RprDesc,
                        RprLinea = s.MobRproductos.RprLinea,
                        RprSubl = s.MobRproductos.RprSubl,
                        RprNuevo = s.MobRproductos.RprNuevo,
                        RprComponentes = s.MobRproductos.RprComponentes,
                        RprAplicacion = s.MobRproductos.RprAplicacion,
                        RprMediosPubli = s.MobRproductos.RprMediosPubli,
                        RprUrlVideoClip = s.MobRproductos.RprUrlVideoClip,
                        RprUrlImagen = s.MobRproductos.RprUrlImagen,
                        RprEan = s.MobRproductos.RprEan,
                        MobRlinea = new MobRlinea()
                        {
                            RlnLinea = s.MobRproductos.MobRlinea.RlnLinea,
                            RlnDescr = s.MobRproductos.MobRlinea.RlnDescr,
                        },
                        MobRsubLineas = new MobRsubLineas()
                        {
                            RsbLinea = s.MobRproductos.MobRsubLineas.RsbLinea,
                            RsbSubl = s.MobRproductos.MobRsubLineas.RsbSubl,
                            RsbDescr = s.MobRproductos.MobRsubLineas.RsbDescr,
                        },
                    }                    
                }
                );
            //.Include("MobRprodCust.MobRproductos");               


            return queryResult;
        }
    }
}
