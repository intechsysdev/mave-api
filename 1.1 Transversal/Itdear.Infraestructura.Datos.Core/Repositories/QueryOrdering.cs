using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Itdear.Infraestructura.Datos.Core.Repositories
{
    /// <summary>
    /// Ordenamiento por nombre de propiedad. La consulta paginada recibe el campo de
    /// ordenamiento como texto desde el cliente, asi que hay que construir el
    /// selector de llave en tiempo de ejecucion.
    /// </summary>
    public static class QueryOrdering
    {
        /// <summary>
        /// Aplica el ordenamiento indicado. Si el nombre viene vacio o la propiedad no
        /// existe en la entidad, devuelve la consulta sin tocar: es preferible entregar
        /// resultados sin ordenar que tumbar la peticion por un parametro de la UI.
        /// </summary>
        public static IQueryable<TEntity> ApplyOrderBy<TEntity>(
            IQueryable<TEntity> query,
            string propertyName,
            bool ascending)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            Expression member = parameter;

            // Se admite notacion con punto para ordenar por una propiedad de una navegacion.
            foreach (var part in propertyName.Split('.'))
            {
                var property = member.Type.GetProperty(
                    part,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (property == null)
                {
                    return query;
                }

                member = Expression.Property(member, property);
            }

            var keySelector = Expression.Lambda(member, parameter);

            var method = ascending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);

            var call = Expression.Call(
                typeof(Queryable),
                method,
                new[] { typeof(TEntity), member.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<TEntity>(call);
        }
    }
}
