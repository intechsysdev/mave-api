using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmMnapp : Entity
    {
        public int MnappNappId { get; set; }
        public int MnappVappId { get; set; }
        public int MnappTappId { get; set; }
        public string MnappDescription { get; set; }
        public DateTime MnappDate { get; set; }
        public string MnappImage { get; set; }
    }
}
