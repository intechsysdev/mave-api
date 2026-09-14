using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmRacceso : Entity
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
