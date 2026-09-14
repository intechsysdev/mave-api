using Itdear.Infraestructura.Datos.Core.Repositories;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.ModuloMob.Repositories;
using ECM.Dominio.UnitsOfWork;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECM.Infraestructura.Datos.Repositories.ModuloMob
{
    public class MobRsubMenuRepository : Repository<MobRsubMenu>, IMobRsubMenuRepository
    {
        public MobRsubMenuRepository(IUnitOfWorkECM unitOfWork) : base(unitOfWork)
        {

        }

        public IQueryable<MobRsubMenu> MenuPerfil(string cmpy, string codper)
        {
            var setPerfilMenu = _unitOfWork.Set<MobRperfilMenuPpal>();

            var setSubMenu = _unitOfWork.Set<MobRsubMenu>();

            var queryPerfilMenu = setPerfilMenu.AsQueryable();

            var querySubMenu = queryPerfilMenu.Where(w => w.RpfmCmpy == cmpy && w.RpfmCodper == codper).Join(
                     setSubMenu,
                     pm => new { cmpy = pm.RpfmCmpy, codmen = pm.RpfmCodmen, codsubmen = pm.RpfmCodsubmen },
                     sm => new { cmpy = sm.RsubmCmpy, codmen = sm.RsubmCodmen, codsubmen = sm.RsubmCodsubmen },
                     (c, s) => new { c, s })
                     //.Where(w => w.ue.IdUsuario == idUsuario && w.ue.Activo && (idEmpresa == null || w.ue.IdEmpresa == idEmpresa))
                     .Select(s => s.s)
                     .Include(i => i.MobRmenuPpal);

            return querySubMenu;
        }
    }
}
