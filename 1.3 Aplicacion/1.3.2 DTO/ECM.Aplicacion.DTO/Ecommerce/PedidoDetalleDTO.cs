namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class PedidoDetalleDTO
    {
        public ProductoDTO Producto { get; set; }
    
        public int Cantidad { get; set; }

        public decimal ValorUnitario { get; set; }

        public bool AplicaIva { get; set; }
    }
}
