using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRcustUserRepository : Repository<MobRcustUser>, IMobRcustUserRepository
    {
        public MobRcustUserRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }
    }
}
