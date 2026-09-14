using Itdear.Dominio.Core.Entities;
using System;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRordHeadCust: Entity
    {
        public MobRordHeadCust()
        {
            MobRordLineCust = new HashSet<MobRordLineCust>();
        }

        public string OrhCmpy { get; set; }
        public int OrhNum { get; set; }
        public string OrhCust { get; set; }
        public string OrhShip { get; set; }
        public DateTime? OrhEntdt { get; set; }
        public string OrhTime { get; set; }
        public string OrhCom { get; set; }
        public DateTime? OrhShpd { get; set; }
        public string OrhPo { get; set; }
        public decimal? OrhTaxp { get; set; }
        public decimal? OrhSchrg { get; set; }
        public string OrhToken { get; set; }
        public int OrhNum2 { get; set; }
        public string OrhMarca { get; set; }
        public DateTime? OrhDate { get; set; }
        public ICollection<MobRordLineCust> MobRordLineCust { get; set; }
    }
}
