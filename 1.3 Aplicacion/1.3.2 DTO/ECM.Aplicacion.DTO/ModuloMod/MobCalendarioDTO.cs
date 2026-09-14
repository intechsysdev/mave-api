using System;

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobCalendarioDTO
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
