using Itdear.Dominio.Core.Entities;
using System;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRusuarios : Entity
    {
        public string RusuCmpy { get; set; }
        public string RusuCodusu { get; set; }
        public string RusuPasswd { get; set; }
        public string RusuNombre { get; set; }
        public string RusuCodper { get; set; }
        public string RusuCorreo { get; set; }
        public string RusuReg { get; set; }
        public string RusuTeri { get; set; }
        public string RusuEnter { get; set; }
        public DateTime RusuDate { get; set; }
        public string RusuTime { get; set; }
        public string RusuEstadoupd { get; set; }
        public MobRperfiles MobRperfiles { get; set; }
    }
}
