using System.Collections.Generic;
using System.Linq;

namespace Itdear.Dominio.Core.Repositories
{
    /// <summary>
    /// Pagina de resultados ya materializada.
    /// </summary>
    public sealed class PagedResult<TEntity> : IPagedResult<TEntity>
    {
        public PagedResult(int totalItems, int page, int pageSize, IEnumerable<TEntity> items)
        {
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
            Items = items ?? Enumerable.Empty<TEntity>();
        }

        public int TotalItems { get; }

        public int Page { get; }

        public int PageSize { get; }

        public IEnumerable<TEntity> Items { get; }
    }
}
