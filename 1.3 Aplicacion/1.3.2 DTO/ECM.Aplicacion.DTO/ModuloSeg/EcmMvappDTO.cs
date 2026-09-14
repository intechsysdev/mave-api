using System;

namespace ECM.Aplicacion.DTO.ModuloSeg
{
    public class EcmMvappDTO
    {
        public int RvappId { get; set; }
        public int RvappTappId { get; set; }
        public string RvappDescription { get; set; }
        public DateTime RvappDate { get; set; }
        public string RvappVersion { get; set; }
        public string RvappToken { get; set; }
    }
}
