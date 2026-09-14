using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRusuariosRepository : Repository<MobRusuarios>, IMobRusuariosRepository
    {
        public MobRusuariosRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }        
    }
}
