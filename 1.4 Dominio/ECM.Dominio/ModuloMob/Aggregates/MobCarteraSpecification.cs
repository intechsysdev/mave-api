using ECM.Dominio.ModuloMob.Entities;
using Itdear.Dominio.Core.Specifications;
using System;

namespace ECM.Dominio.ModuloMob.Aggregates
{
    public class MobCarteraSpecification : SpecificationBase<MobCartera>
    {
        public MobCarteraSpecification(string textoBusqueda, int? mesesCartera, string cmpy, string cust, string succli)
        {
            Criteria = BusquedaTextoCompleto(textoBusqueda, mesesCartera, cmpy, cust, succli).SatisfiedBy();
        }

        private ISpecificationCriteria<MobCartera> BusquedaTextoCompleto(string texto, int? mesesCartera, string cmpy, string cust, string succli)
        {
            SpecificationCriteria<MobCartera> especificacion = new SpecificationCriteriaTrue<MobCartera>();

            var spl = texto.ToLower().Trim().Split(' ');

            foreach (var s in spl)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    //SpecificationCriteria<MobCartera> especificacionSpl = new SpecificationCriteriaTrue<MobCartera>();
                    especificacion &= new SpecificationCriteriaDirect<MobCartera>(c => c.CarTipo.Contains(s) || c.CarNumero.ToString().Contains(s) || c.CarGuia.Contains(s));
                    //var eEspecificacion2 = new SpecificationCriteriaDirect<MobCartera>(c => c.CarNumero.ToString().Contains(s));
                    //var eEspecificacion3 = new SpecificationCriteriaDirect<MobCartera>(c => c.CarGuia.Contains(s));

                    //eEspecificacion1;

                    //especificacion &= especificacionSpl;
                }
            }

            especificacion &= new SpecificationCriteriaDirect<MobCartera>(c => c.CarCmpy == cmpy && c.CarCust == cust && c.CarSuccli == succli);
            
            if (mesesCartera != null)
            {
                var fecha = DateTime.Now.Date;

                fecha = fecha.AddMonths(mesesCartera.GetValueOrDefault() * -1);
                especificacion &= new SpecificationCriteriaDirect<MobCartera>(c => c.CarFechaven >= fecha);
            }

            return especificacion;
        }
    }
}