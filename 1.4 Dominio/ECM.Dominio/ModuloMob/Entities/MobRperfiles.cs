using Itdear.Dominio.Core.Entities;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRperfiles : Entity
    {
        public MobRperfiles()
        {
            MobRusuarios = new HashSet<MobRusuarios>();
            MobRperfilMenuPpal = new HashSet<MobRperfilMenuPpal>();
        }

        public string RperCmpy { get; set; }
        public string RperCodPer { get; set; }
        public string RperDesPer { get; set; }
        public ICollection<MobRusuarios> MobRusuarios { get; set; }
        public ICollection<MobRperfilMenuPpal> MobRperfilMenuPpal { get; set; }
    }
}
