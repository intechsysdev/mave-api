using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobPromocionesRepository : Repository<MobPromociones>, IMobPromocionesRepository
    {
        public MobPromocionesRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }
    }
}
