using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRperfilMenuPpal : Entity
    {
        public string RpfmCmpy { get; set; }
        public string RpfmCodper { get; set; }
        public string RpfmCodmen { get; set; }
        public string RpfmCodsubmen { get; set; }
        public string RpfmEnter { get; set; }
        public DateTime? RpfmDate { get; set; }
        public string RpfmTime { get; set; }
        public string RpfmEstadoupd { get; set; }
        public MobRperfiles MobRperfiles { get; set; }
    }
}
