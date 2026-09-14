

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobRcustUserDTO
    {
        public string MrcuCmpy { get; set; }
        public string MrcuCust { get; set; }
        public string MrcuSuccli { get; set; }
        public string MrcuAutorizado { get; set; }
        public string MrcuName { get; set; }
        public string MrcuTele { get; set; }
        public string MrcuEmail { get; set; }
        public decimal? MrcuVrMinimo { get; set; }
        public decimal? MrcuVrMaximo { get; set; }
        public decimal? MrcuSchrc { get; set; }
        public decimal? MrcuIva { get; set; }               
        public string MrcuType { get; set; }
        public string MrcuTipCliente { get; set; }
        public string MrcuLevel { get; set; }
        public short MrcuDiasEnt { get; set; }        
        public string MrcuCodPer { get; set; }
    }
}
