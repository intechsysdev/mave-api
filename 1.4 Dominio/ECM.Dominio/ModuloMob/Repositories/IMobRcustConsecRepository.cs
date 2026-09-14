using Itdear.Dominio.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;

namespace ECM.Dominio.ModuloMob.Repositories
{
    public interface IMobRcustConsecRepository : IRepositoryAsync<MobRcustConsec>
    {
        int ConsecutivoMax(string cmpy, string cust, string succli, string tipdoc);        
    }
}
