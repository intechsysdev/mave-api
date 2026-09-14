using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRprodCustRepository : Repository<MobRprodCust>, IMobRprodCustRepository
    {
        public MobRprodCustRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }
    }
}
