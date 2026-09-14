

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobRproductosDTO
    {
        public string RprCmpy { get; set; }
        public string RprId { get; set; }
        public string RprDesc { get; set; }
        public string RprLinea { get; set; }
        public string RprSubl { get; set; }
        public bool RprNuevo { get; set; }
        public string RprComponentes { get; set; }
        public string RprAplicacion { get; set; }
        public string RprMediosPubli { get; set; }
        public string RprUrlVideoClip { get; set; }
        public string RprUrlImagen { get; set; }
        public byte[] RprImagen { get; set; }
        public string RprEan { get; set; }
        public MobRlineaDTO MobRlinea { get; set; }
        public MobRsubLineasDTO MobRsubLineas { get; set; }
    }
}
