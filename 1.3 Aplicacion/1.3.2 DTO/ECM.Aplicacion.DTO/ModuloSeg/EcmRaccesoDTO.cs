using System;

namespace ECM.Aplicacion.DTO.ModuloSeg
{
    public class EcmRaccesoDTO
    {
        public string RacceEmprId { get; set; }
        public string RacceUsuaCust { get; set; }
        public string RacceUsuaSuccli { get; set; }
        public string RacceMac { get; set; }
        public DateTime RacceDate { get; set; }
        public int RacceVappId { get; set; }
        public int RacceTappId { get; set; }
    }
}
