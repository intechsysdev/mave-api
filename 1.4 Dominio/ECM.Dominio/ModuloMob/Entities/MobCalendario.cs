using Itdear.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECM.Dominio.ModuloMob.Entities
{
    public partial class MobCalendario : Entity
    {
        public string McaCmpy { get; set; }
        public int McaAno { get; set; }
        public int McaMes { get; set; }
        public DateTime? McaFecIni { get; set; }
        public DateTime? McaFecFin { get; set; }
        public DateTime? McaFecEnt { get; set; }
        public DateTime? McaFecTem { get; set; }
        public DateTime? McaFecCier { get; set; }
    }
}
