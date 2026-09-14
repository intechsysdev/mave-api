using System;
using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        public DateTime Fecha { get; set; }

        public string  Marca { get; set; }

        public decimal PorcDescuento { get; set; }

        public decimal PorcIva { get; set; }

        public decimal TotalAux { get; set; }

        public string Observaciones { get; set; }

        public bool Cerrado { get; set; }

        public string FormatDate { get; set; }

        public string FormatCurrency { get; set; }

        public string FormatNumber { get; set; }

        public IEnumerable<PedidoDetalleDTO> PedidoDetalle { get; set; }
    }
}
