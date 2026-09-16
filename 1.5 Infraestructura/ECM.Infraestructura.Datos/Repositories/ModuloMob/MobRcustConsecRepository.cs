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
            // Se proyecta a int? para que el MAX de un conjunto vacio llegue como null y
            // se resuelva en 0: es el caso del cliente que aun no tiene consecutivo.
            var consecutivo = Queryable()
                .Where(e => e.MdcCmpy == cmpy && e.MdcCust == cust && e.MdcSuccli == succli && e.MdcTipdoc == tipdoc)
                .Max(e => (int?)e.MdcConsec);

            return consecutivo ?? 0;
        }
    }
}
