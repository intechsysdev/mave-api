using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Linq;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRcustConsecRepository : Repository<MobRcustConsec>, IMobRcustConsecRepository
    {
        public MobRcustConsecRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }

        public int ConsecutivoMax(string cmpy, string cust, string succli, string tipdoc)
        {
            int consecutivo = 0;
            var query = Queryable();
            consecutivo = query.Where(e => e.MdcCmpy == cmpy && e.MdcCust == cust && e.MdcSuccli == succli && e.MdcTipdoc == tipdoc).DefaultIfEmpty().Max(e => e.MdcConsec);

            return consecutivo;
        }
    }
}
