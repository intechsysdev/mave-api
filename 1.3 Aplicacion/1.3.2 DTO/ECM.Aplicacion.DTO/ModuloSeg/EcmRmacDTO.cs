using System;

namespace ECM.Aplicacion.DTO.ModuloSeg
{
    public class EcmRmacDTO
    {
        public string RmacEmprId { get; set; }
        public string RmacUsuaCust { get; set; }
        public string RmacUsuaSuccli { get; set; }
        public string RmacMac { get; set; }
        public DateTime RmacDate { get; set; }        
        public string RmacVapp2fa { get; set; }
    }
}
