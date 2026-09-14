using Itdear.Dominio.Core.Entities;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRproductos : Entity
    {
        public MobRproductos()
        {
            MobRprodCust = new HashSet<MobRprodCust>();
            MobRordLineCust = new HashSet<MobRordLineCust>();
        }

        public string RprCmpy { get; set; }
        public string RprId { get; set; }
        public string RprDesc { get; set; }
        public string RprLinea { get; set; }
        public string RprSubl { get; set; }
        public short RprNuevo { get; set; }
        public string RprComponentes { get; set; }
        public string RprAplicacion { get; set; }
        public string RprMediosPubli { get; set; }
        public string RprUrlVideoClip { get; set; }
        public string RprUrlImagen { get; set; }
        public byte[] RprImagen { get; set; }
        public string RprEan { get; set; }
        public MobRlinea MobRlinea { get; set; }
        public MobRsubLineas MobRsubLineas { get; set; }
        public virtual ICollection<MobRprodCust> MobRprodCust { get; set; }
        public virtual ICollection<MobRordLineCust> MobRordLineCust { get; set; }

    }
}
