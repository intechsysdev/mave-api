using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobPromociones : Entity
    {
        public string PrmCmpy { get; set; }
        public DateTime? PrmFecIni { get; set; }
        public DateTime? PrmFecFin { get; set; }
        public string PrmUrlbanner { get; set; }
        public string PrmBanner { get; set; }
        public string PrmEnter { get; set; }
        public DateTime? PrmDate { get; set; }
        public string PrmTime { get; set; }
    }
}
