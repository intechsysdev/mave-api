using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRcustConsec : Entity
    {
        public string MdcCmpy { get; set; }
        public string MdcCust { get; set; }
        public string MdcSuccli { get; set; }
        public string MdcTipdoc { get; set; }
        public int MdcConsec { get; set; }
        public string MdcEnter { get; set; }
        public DateTime? MdcEntdt { get; set; }
        public string MdcTime { get; set; }
    }
}
