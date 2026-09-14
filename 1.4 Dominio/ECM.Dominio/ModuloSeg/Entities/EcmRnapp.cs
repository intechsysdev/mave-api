using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmRnapp : Entity
    {
        public string RnappEmprId { get; set; }
        public string RnappUsuaCust { get; set; }
        public string RnappUsuaSuccli { get; set; }
        public string RnappMac { get; set; }
        public DateTime RnappDate { get; set; }
        public string RnappApprove { get; set; }
        public int RnappVappId { get; set; }
        public int RnappTappId { get; set; }
        public int RnappNappId { get; set; }
    }
}
