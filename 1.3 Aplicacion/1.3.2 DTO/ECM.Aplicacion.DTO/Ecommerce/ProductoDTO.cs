namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class ProductoDTO
    {
        public string IdProducto { get; set; }
        public string DescProducto { get; set; }
        public string IdLinea { get; set; }
        public string DescLinea { get; set; }
        public string IdSubLinea { get; set; }
        public string DescSubLinea { get; set; }
        public short Nuevo { get; set; }
        public string UrlImagen { get; set; }
        public string Ean { get; set; }
        public decimal Precio { get; set; }
        public string Impuesto { get; set; }
        public bool AplicaIva { get; set; }
        public int CantidadMaxima { get; set; }
        public int _CantidadSolicitada { get; set; }
        public int CantidadSolicitada { get; set; }        
        public int UnidadesEmpaque { get; set; }
    }
}
