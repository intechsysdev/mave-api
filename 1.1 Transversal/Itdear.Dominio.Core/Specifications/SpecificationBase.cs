using System;
using System.Linq.Expressions;

namespace Itdear.Dominio.Core.Specifications
{
    /// <summary>
    /// Base de las especificaciones del dominio. La clase derivada arma el criterio
    /// en su constructor y lo deja en <see cref="Criteria"/>.
    /// </summary>
    public abstract class SpecificationBase<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        protected SpecificationBase()
        {
            Criteria = entity => true;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; protected set; }
    }
}
