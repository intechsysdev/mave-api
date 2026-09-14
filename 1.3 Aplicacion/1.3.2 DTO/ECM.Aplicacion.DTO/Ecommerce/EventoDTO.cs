using System;

namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class EventoDTO
    {
        public string IdTipo { get; set; }

        public DateTime FechaIni { get; set; }

        public DateTime FechaFin { get; set; }

        public string DescEvento { get; set; }

        public ProductoDTO Producto { get; set; }
    }
}
