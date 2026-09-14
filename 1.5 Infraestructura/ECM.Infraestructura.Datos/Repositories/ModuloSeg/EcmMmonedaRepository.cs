using ECM.Dominio.ModuloSeg.Entities;
using ECM.Dominio.ModuloSeg.Repositories;
using ECM.Dominio.UnitsOfWork;
using Itdear.Infraestructura.Datos.Core.Repositories;

namespace ECM.Infraestructura.Datos.Repositories.ModuloSeg
{

    public class EcmMmonedaRepository : Repository<EcmMmoneda>, IEcmMmonedaRepository
    {
        public EcmMmonedaRepository(IUnitOfWorkSEG unitOfWork) : base(unitOfWork)
        {

        }
    }
}
