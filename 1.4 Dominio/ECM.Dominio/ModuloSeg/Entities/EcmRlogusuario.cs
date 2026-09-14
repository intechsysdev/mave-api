using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmRlogusuario : Entity
    {
        public string RlogussEmprId { get; set; }
        public string RlogusUsuaCust { get; set; }
        public string RlogusUsuaSuccli { get; set; }
        public string RlogusMac { get; set; }
        public DateTime RlogusDate { get; set; }
        public string RlogusData { get; set; }
        public string RlogusValue { get; set; }
        public string RlogusDescription { get; set; }
    }
}
