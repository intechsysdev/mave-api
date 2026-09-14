

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobRprodCustDTO
    {
        public string MrpcCmpy { get; set; }
        public string MrpcCust { get; set; }
        public string MrpcSuccli { get; set; }
        public string MrpcId { get; set; }
        public decimal MrpcPrecio { get; set; }
        public string MrpcTax { get; set; }
        public int MrpcCantMax { get; set; }
        public string MrpcEan { get; set; }
        public short MrpcUnidemPaq { get; set; }
        public MobRproductosDTO MobRproductos { get; set; }
    }
}
