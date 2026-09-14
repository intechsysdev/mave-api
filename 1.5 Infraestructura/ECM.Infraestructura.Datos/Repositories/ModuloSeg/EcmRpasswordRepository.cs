using ECM.Dominio.ModuloSeg.Entities;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Infraestructura.Datos.Core.Repositories;

namespace ECM.Infraestructura.Datos.Repositories.ModuloSeg
{

    public class EcmRpasswordRepository : Repository<EcmRpassword>, IEcmRpasswordRepository
    {
        public EcmRpasswordRepository(IUnitOfWorkSEG unitOfWork) : base(unitOfWork)
        {

        }
    }
}
