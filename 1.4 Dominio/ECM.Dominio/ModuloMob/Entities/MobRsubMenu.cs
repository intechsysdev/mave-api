using Itdear.Dominio.Core.Entities;
using System;
using System.Collections.Generic;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobRsubMenu : Entity
    {
        public string RsubmCmpy { get; set; }
        public short RsubmIdioma { get; set; }
        public string RsubmCodmen { get; set; }
        public string RsubmCodsubmen { get; set; }
        public string RsubmDessubmen { get; set; }
        public short? RsubmCabecera { get; set; }
        public string RsubmAplicacion { get; set; }
        public string RsubmEnter { get; set; }
        public DateTime? RsubmDate { get; set; }
        public string RsubmTime { get; set; }
        public string RsubmEstadoupd { get; set; }
        public string RsubmIcono { get; set; }
        public string RsubmAplicacionMovil { get; set; }
        public string RsubmIconoMovil { get; set; }
        public MobRmenuPpal MobRmenuPpal { get; set; }        
    }
}
