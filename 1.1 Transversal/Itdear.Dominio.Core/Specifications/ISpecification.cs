using System;
using System.Linq.Expressions;

namespace Itdear.Dominio.Core.Specifications
{
    /// <summary>
    /// Criterio de consulta componible. Permite construir el filtro de una consulta
    /// combinando condiciones sueltas con los operadores and, or y not.
    /// </summary>
    public interface ISpecificationCriteria<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }

    /// <summary>
    /// Especificacion de consulta: expone el criterio ya resuelto como expresion.
    /// </summary>
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
