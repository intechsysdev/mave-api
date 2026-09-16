using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Itdear.Dominio.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Itdear.Infraestructura.Datos.Core.Repositories
{
    /// <summary>
    /// Envoltura de un <see cref="IQueryable{T}"/> que anade las operaciones de consulta
    /// de uso frecuente como metodos de instancia.
    ///
    /// Delega Provider y Expression en la consulta original, de modo que sigue siendo una
    /// consulta de EF Core: los operadores Include / ThenInclude / Where y los metodos
    /// asincronos del proveedor funcionan igual sobre este tipo.
    /// </summary>
    internal sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class
    {
        private readonly IQueryable<TEntity> _query;

        public QueryFluent(IQueryable<TEntity> query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public Type ElementType => _query.ElementType;

        public Expression Expression => _query.Expression;

        public IQueryProvider Provider => _query.Provider;

        public IEnumerator<TEntity> GetEnumerator() => _query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_query).GetEnumerator();

        public IQueryFluent<TEntity> OrderBy(string propertyName, bool ascending = true)
        {
            return new QueryFluent<TEntity>(QueryOrdering.ApplyOrderBy(_query, propertyName, ascending));
        }

        public IQueryFluent<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector, bool ascending = true)
        {
            var ordered = ascending ? _query.OrderBy(keySelector) : _query.OrderByDescending(keySelector);

            return new QueryFluent<TEntity>(ordered);
        }

        public IQueryFluent<TEntity> IncludePath(Expression<Func<TEntity, object>> path)
        {
            return new QueryFluent<TEntity>(_query.Include(path));
        }

        public TEntity FirstOrDefault() => _query.FirstOrDefault();

        public Task<TEntity> FirstOrDefaultAsync() => _query.FirstOrDefaultAsync();

        public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken)
            => _query.FirstOrDefaultAsync(cancellationToken);

        public Task<IEnumerable<TEntity>> SelectAsync() => SelectAsync(CancellationToken.None);

        public async Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<IPagedResult<TEntity>> SelectPageAsync(int page, int pageSize)
            => SelectPageAsync(page, pageSize, CancellationToken.None);

        public async Task<IPagedResult<TEntity>> SelectPageAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            // La pagina llega base 1 desde el cliente; una pagina menor a 1 se trata como la primera.
            var currentPage = page < 1 ? 1 : page;
            var currentSize = pageSize < 1 ? 1 : pageSize;

            var total = await _query.CountAsync(cancellationToken).ConfigureAwait(false);

            var items = await _query
                .Skip((currentPage - 1) * currentSize)
                .Take(currentSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new PagedResult<TEntity>(total, currentPage, currentSize, items);
        }

        public Task<int> CountAsync() => _query.CountAsync();
    }
}
