using Itdear.Dominio.Core.Entities;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRlinea : Entity
    {
        public MobRlinea()
        {
            MobRproductos = new HashSet<MobRproductos>();
            MobRsubLineas = new HashSet<MobRsubLineas>();
        }

        public string RlnCmpy { get; set; }
        public string RlnLinea { get; set; }
        public string RlnDescr { get; set; }
        public virtual ICollection<MobRproductos> MobRproductos { get; set; }
        public virtual ICollection<MobRsubLineas> MobRsubLineas { get; set; }
    }
}
