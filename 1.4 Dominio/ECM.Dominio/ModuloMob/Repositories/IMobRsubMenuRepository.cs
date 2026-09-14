using Itdear.Dominio.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using System.Linq;

namespace ECM.Dominio.ModuloMob.Repositories
{
    public interface IMobRsubMenuRepository : IRepositoryAsync<MobRsubMenu>
    {
        IQueryable<MobRsubMenu> MenuPerfil(string cmpy, string codper);
    }
}
