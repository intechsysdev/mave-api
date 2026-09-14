using System;

namespace ECM.Aplicacion.DTO.ModuloApp
{
    public class NovedadAppDTO
    {
        //public int IdVersionAplicacion { get; set; }
        //public int IdTipoAplicacion { get; set; }
        public int IdNovedad { get; set; }
        public string DescNovedad { get; set; }
        public string RutaImagen { get; set; }
        public DateTime Fecha { get; set; }
        public bool Aprobada { get; set; }
    }
}
