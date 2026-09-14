using System;

namespace ECM.Aplicacion.DTO.ModuloMob
{
    public class MobCarteraDTO
    {
        public string CarCmpy { get; set; }
        public string CarCodusu { get; set; }
        public string CarTipo { get; set; }
        public double CarNumero { get; set; }
        public string CarCust { get; set; }
        public string CarSuccli { get; set; }
        public string CarGuia { get; set; }
        public DateTime? CarFecha { get; set; }
        public double? CarDiasven { get; set; }
        public decimal? CarAmt { get; set; }
        public decimal? CarSaldo { get; set; }
        public decimal? CarNotacredito { get; set; }
        public string CarAplicanc { get; set; }
        public DateTime? CarFechaven { get; set; }
        public decimal CarPorciva { get; set; }
    }
}
