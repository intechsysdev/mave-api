using System;
using System.Linq.Expressions;

namespace Itdear.Dominio.Core.Specifications
{
    /// <summary>
    /// Base de los criterios componibles. Los operadores devuelven un criterio nuevo,
    /// de manera que acumular con "criterio &amp;= otro" va sumando condiciones.
    /// </summary>
    public abstract class SpecificationCriteria<TEntity> : ISpecificationCriteria<TEntity>
        where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> SatisfiedBy();

        public static SpecificationCriteria<TEntity> operator &(
            SpecificationCriteria<TEntity> left,
            SpecificationCriteria<TEntity> right)
        {
            return new SpecificationCriteriaAnd<TEntity>(left, right);
        }

        public static SpecificationCriteria<TEntity> operator |(
            SpecificationCriteria<TEntity> left,
            SpecificationCriteria<TEntity> right)
        {
            return new SpecificationCriteriaOr<TEntity>(left, right);
        }

        public static SpecificationCriteria<TEntity> operator !(SpecificationCriteria<TEntity> criteria)
        {
            return new SpecificationCriteriaNot<TEntity>(criteria);
        }

        // Requeridos por el compilador para poder usar los operadores anteriores
        // en su forma condicional.
        public static bool operator false(SpecificationCriteria<TEntity> criteria) => false;

        public static bool operator true(SpecificationCriteria<TEntity> criteria) => false;
    }

    /// <summary>Criterio que siempre se cumple. Elemento neutro al acumular condiciones.</summary>
    public sealed class SpecificationCriteriaTrue<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        public override Expression<Func<TEntity, bool>> SatisfiedBy() => entity => true;
    }

    /// <summary>Criterio que nunca se cumple.</summary>
    public sealed class SpecificationCriteriaFalse<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        public override Expression<Func<TEntity, bool>> SatisfiedBy() => entity => false;
    }

    /// <summary>Criterio construido directamente a partir de una expresion lambda.</summary>
    public sealed class SpecificationCriteriaDirect<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _criteria;

        public SpecificationCriteriaDirect(Expression<Func<TEntity, bool>> criteria)
        {
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy() => _criteria;
    }

    internal sealed class SpecificationCriteriaAnd<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        private readonly SpecificationCriteria<TEntity> _left;
        private readonly SpecificationCriteria<TEntity> _right;

        public SpecificationCriteriaAnd(SpecificationCriteria<TEntity> left, SpecificationCriteria<TEntity> right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return ExpressionCombiner.Combine(_left.SatisfiedBy(), _right.SatisfiedBy(), Expression.AndAlso);
        }
    }

    internal sealed class SpecificationCriteriaOr<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        private readonly SpecificationCriteria<TEntity> _left;
        private readonly SpecificationCriteria<TEntity> _right;

        public SpecificationCriteriaOr(SpecificationCriteria<TEntity> left, SpecificationCriteria<TEntity> right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return ExpressionCombiner.Combine(_left.SatisfiedBy(), _right.SatisfiedBy(), Expression.OrElse);
        }
    }

    internal sealed class SpecificationCriteriaNot<TEntity> : SpecificationCriteria<TEntity>
        where TEntity : class
    {
        private readonly SpecificationCriteria<TEntity> _criteria;

        public SpecificationCriteriaNot(SpecificationCriteria<TEntity> criteria)
        {
            _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            var inner = _criteria.SatisfiedBy();

            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(inner.Body), inner.Parameters);
        }
    }

    /// <summary>
    /// Une dos lambdas en una sola reescribiendo el parametro de la segunda para que
    /// apunte al de la primera. Sin esa reescritura el arbol resultante quedaria con
    /// dos parametros distintos y el proveedor de EF no podria traducirlo.
    /// </summary>
    internal static class ExpressionCombiner
    {
        public static Expression<Func<TEntity, bool>> Combine<TEntity>(
            Expression<Func<TEntity, bool>> left,
            Expression<Func<TEntity, bool>> right,
            Func<Expression, Expression, BinaryExpression> merge)
        {
            var parameter = left.Parameters[0];

            var rewrittenRight = new ParameterRebinder(right.Parameters[0], parameter).Visit(right.Body);

            return Expression.Lambda<Func<TEntity, bool>>(merge(left.Body, rewrittenRight), parameter);
        }

        private sealed class ParameterRebinder : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly ParameterExpression _to;

            public ParameterRebinder(ParameterExpression from, ParameterExpression to)
            {
                _from = from;
                _to = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _from ? _to : base.VisitParameter(node);
            }
        }
    }
}
