using System.Collections.Generic;

namespace ECM.Aplicacion.DTO.Ecommerce
{
    public class LineaDTO
    {
        public string IdLinea { get; set; }
        public string DescLinea { get; set; }
        public List<SubLineaDTO> SubLineas { get; set; }
    }
}
