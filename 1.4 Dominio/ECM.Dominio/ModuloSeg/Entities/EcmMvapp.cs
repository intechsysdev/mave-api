using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmMvapp : Entity
    {
        public int RvappId { get; set; }
        public int RvappTappId { get; set; }
        public string RvappDescription { get; set; }
        public DateTime RvappDate { get; set; }
        public string RvappVersion { get; set; }
        public string RvappToken { get; set; }
    }
}
