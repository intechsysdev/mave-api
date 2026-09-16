using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Itdear.Dominio.Core.Repositories
{
    /// <summary>
    /// Pagina de resultados devuelta por SelectPageAsync.
    /// </summary>
    public interface IPagedResult<TEntity>
    {
        /// <summary>Total de registros que cumplen el filtro, sin paginar.</summary>
        int TotalItems { get; }

        /// <summary>Numero de pagina solicitado, base 1.</summary>
        int Page { get; }

        /// <summary>Tamano de pagina solicitado.</summary>
        int PageSize { get; }

        /// <summary>Registros de la pagina.</summary>
        IEnumerable<TEntity> Items { get; }
    }

    /// <summary>
    /// Consulta en construccion sobre un repositorio.
    ///
    /// Hereda de <see cref="IQueryable{T}"/> a proposito: asi los operadores de EF Core
    /// (Include, ThenInclude, Where, Select, ...) siguen aplicando sobre el resultado de
    /// Query() sin necesidad de reimplementarlos aqui. Los metodos de instancia cubren
    /// las operaciones que el codigo de aplicacion invoca sin importar el espacio de
    /// nombres de EF Core.
    /// </summary>
    public interface IQueryFluent<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        /// <summary>Ordena por una propiedad indicada por nombre.</summary>
        IQueryFluent<TEntity> OrderBy(string propertyName, bool ascending = true);

        /// <summary>Ordena por una expresion de seleccion de llave.</summary>
        IQueryFluent<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector, bool ascending = true);

        /// <summary>Incluye una navegacion en la consulta.</summary>
        IQueryFluent<TEntity> IncludePath(Expression<Func<TEntity, object>> path);

        TEntity FirstOrDefault();

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> SelectAsync();

        Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken);

        Task<IPagedResult<TEntity>> SelectPageAsync(int page, int pageSize);

        Task<IPagedResult<TEntity>> SelectPageAsync(int page, int pageSize, CancellationToken cancellationToken);

        Task<int> CountAsync();
    }
}
