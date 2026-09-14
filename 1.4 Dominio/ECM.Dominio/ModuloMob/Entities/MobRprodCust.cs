using Itdear.Dominio.Core.Entities;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRprodCust : Entity
    {
        public MobRprodCust()
        {
            MobRordLineCustShop = new HashSet<MobRordLineCustShop>();
        }

        public string MrpcCmpy { get; set; }
        public string MrpcCust { get; set; }
        public string MrpcSuccli { get; set; }
        public string MrpcId { get; set; }
        public decimal MrpcPrecio { get; set; }
        public string MrpcTax { get; set; }
        public int MrpcCantMax { get; set; }
        public string MrpcEan { get; set; }
        public short MrpcUnidemPaq { get; set; }
        public MobRproductos MobRproductos { get; set; }
        public virtual ICollection<MobRordLineCustShop> MobRordLineCustShop { get; set; }
    }
}
