using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmRpassword : Entity
    {
        public string RpassEmprId { get; set; }
        public string RpassUsuaCust { get; set; }
        public string RpassUsuaSuccli { get; set; }
        public DateTime RpassDate { get; set; }
        public string RpassPassword { get; set; }
        public string RpassCode { get; set; }
    }
}
