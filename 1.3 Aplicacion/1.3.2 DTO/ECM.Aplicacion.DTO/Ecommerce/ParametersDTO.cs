namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class ParametersDTO
    {
        public bool TakeOrders { get; set; }

        public decimal MinValueOrder { get; set; }

        public decimal MaxValueOrder { get; set; }

        public decimal DiscountRate { get; set; }

        public decimal TaxRate { get; set; }
    }
}
