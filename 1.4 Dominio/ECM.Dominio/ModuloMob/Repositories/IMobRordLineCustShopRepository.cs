using Itdear.Dominio.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using System.Linq;

namespace ECM.Dominio.ModuloMob.Repositories
{
    public interface IMobRordLineCustShopRepository : IRepositoryAsync<MobRordLineCustShop>
    {

        IQueryable<MobRordLineCustShop> ConsultarCarrito(string cmpy, string cust, string succli);

    }
}
