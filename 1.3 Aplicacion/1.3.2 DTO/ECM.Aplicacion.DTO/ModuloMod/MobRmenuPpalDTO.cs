
using System;
using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobRmenuPpalDTO
    {
        public string RmenCmpy { get; set; }
        public short RmenIdioma { get; set; }
        public string RmenCodmen { get; set; }
        public string RmenDesmen { get; set; }
        public short? RmenCabecera { get; set; }
        public string RmenAplicacion { get; set; }
        public string RmenEnter { get; set; }
        public DateTime? RmenDate { get; set; }
        public string RmenTime { get; set; }
        public string RmenEstadoupd { get; set; }
        public string RmenIcono { get; set; }
        public string RmenAplicacionMovil { get; set; }
        public string RmenIconoMovil { get; set; }
        public List<MobRsubMenuDTO> MobRsubMenu { get; set; }
    }
}
