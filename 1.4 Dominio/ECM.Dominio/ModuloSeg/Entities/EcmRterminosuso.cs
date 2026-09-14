using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmRterminosuso : Entity
    {
        public string RtermEmprId { get; set; }
        public string RtermUsuaCust { get; set; }
        public string RtermUsuaSuccli { get; set; }
        public string RtermMac { get; set; }
        public string RtermApprove { get; set; }
        public DateTime RtermDate { get; set; }
    }
}
