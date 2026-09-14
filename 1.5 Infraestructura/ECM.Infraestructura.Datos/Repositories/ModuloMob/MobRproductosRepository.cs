using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRproductosRepository : Repository<MobRproductos>, IMobRproductosRepository
    {
        public MobRproductosRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }
    }
}
