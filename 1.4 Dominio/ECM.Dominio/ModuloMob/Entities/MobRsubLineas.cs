using Itdear.Dominio.Core.Entities;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRsubLineas : Entity
    {
        public MobRsubLineas()
        {
            MobRproductos = new HashSet<MobRproductos>();
        }

        public string RsbCmpy { get; set; }
        public string RsbLinea { get; set; }
        public string RsbSubl { get; set; }
        public string RsbDescr { get; set; }
        public MobRlinea MobRlinea { get; set; }
        public virtual ICollection<MobRproductos> MobRproductos { get; set; }
    }
}
