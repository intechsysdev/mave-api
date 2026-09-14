using Itdear.Dominio.Core.Entities;

namespace ECM.Dominio.ModuloSeg.Entities
{
    public partial class EcmMcontacto : Entity
    {
        public int McontaId { get; set; }
        public string McontEmprId { get; set; }
        public string McontName { get; set; }
        public string McontMail { get; set; }
        public string McontPhon { get; set; }
        public string McontIden { get; set; }
    }
}