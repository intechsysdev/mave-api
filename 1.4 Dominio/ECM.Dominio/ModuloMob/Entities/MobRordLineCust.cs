using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRordLineCust : Entity
    {
        public string OrlCmpy { get; set; }
        public int OrlNum { get; set; }
        public string OrlCust { get; set; }
        public string OrlShip { get; set; }
        public string OrlPart { get; set; }
        public DateTime? OrlEntdt { get; set; }
        public string OrlTime { get; set; }
        public decimal? OrlQord { get; set; }
        public string OrlTxbl { get; set; }
        public string OrlLevel { get; set; }
        public decimal? OrlUnpr { get; set; }
        public string OrlMarca { get; set; }
        public DateTime? OrlDate { get; set; }
        public MobRordHeadCust MobRordHeadCust { get; set; }

        public MobRproductos MobRproductos { get; set; }
    }
}
