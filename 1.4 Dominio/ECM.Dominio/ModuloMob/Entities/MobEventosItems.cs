using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobEventosItems
        : Entity
    {
        public string EiCmpy { get; set; }
        public string EiType { get; set; }
        public string EiId { get; set; }
        public string EiDesc { get; set; }
        public DateTime? EiFecIni { get; set; }
        public DateTime? EiFecFin { get; set; }
        public string EiEnter { get; set; }
        public DateTime? EiEntdt { get; set; }
        public string EiTime { get; set; }
    }
}
