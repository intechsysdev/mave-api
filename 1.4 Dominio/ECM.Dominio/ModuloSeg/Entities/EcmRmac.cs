using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public class EcmRmac : Entity
    {
        public string RmacEmprId { get; set; }
        public string RmacUsuaCust { get; set; }
        public string RmacUsuaSuccli { get; set; }
        public string RmacMac { get; set; }
        public DateTime RmacDate { get; set; }
        public string RmacVapp2fa { get; set; }
    }
}
