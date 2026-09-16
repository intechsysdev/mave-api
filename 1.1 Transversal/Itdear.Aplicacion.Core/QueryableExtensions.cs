using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Itdear.Dominio.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Itdear.Aplicacion.Core
{
    /// <summary>
    /// Version en metodo de extension de las operaciones de <see cref="IQueryFluent{T}"/>.
    ///
    /// Hacen falta porque al aplicar Include sobre una consulta fluida el resultado pasa
    /// a ser un IQueryable de EF Core y se pierden los metodos de instancia; con estas
    /// extensiones la cadena sigue leyendose igual.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>Materializa la consulta completa.</summary>
        public static async Task<IEnumerable<TEntity>> SelectAsync<TEntity>(
            this IQueryable<TEntity> query,
            CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Materializa una pagina de la consulta junto con el total de registros.</summary>
        public static async Task<IPagedResult<TEntity>> SelectPageAsync<TEntity>(
            this IQueryable<TEntity> query,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var currentPage = page < 1 ? 1 : page;
            var currentSize = pageSize < 1 ? 1 : pageSize;

            var total = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            var items = await query
                .Skip((currentPage - 1) * currentSize)
                .Take(currentSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new PagedResult<TEntity>(total, currentPage, currentSize, items);
        }
    }
}
