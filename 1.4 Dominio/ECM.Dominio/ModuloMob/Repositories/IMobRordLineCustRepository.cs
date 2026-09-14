using Itdear.Dominio.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using System.Linq;

namespace ECM.Dominio.ModuloMob.Repositories
{
    public interface IMobRordLineCustRepository : IRepositoryAsync<MobRordLineCust>
    {
        IQueryable<MobRordLineCust> ConsultarPedido(string cmpy, string cust, string succli, int num);
    }
}
