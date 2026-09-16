using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Itdear.Dominio.Core.Specifications;

namespace Itdear.Dominio.Core.Repositories
{
    /// <summary>
    /// Contrato sincrono de acceso a una entidad del dominio.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>Consulta sin filtro sobre la que se puede seguir componiendo con LINQ.</summary>
        IQueryable<TEntity> Queryable();

        /// <summary>Consulta sin filtro en modo fluido.</summary>
        IQueryFluent<TEntity> Query();

        /// <summary>Consulta filtrada por una expresion.</summary>
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> criteria);

        /// <summary>Consulta filtrada por una especificacion del dominio.</summary>
        IQueryFluent<TEntity> Query(ISpecification<TEntity> specification);

        /// <summary>Busca por llave primaria. Para llaves compuestas se pasan en el orden del mapeo.</summary>
        TEntity Find(params object[] keyValues);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(params object[] keyValues);

        void DeleteRange(IEnumerable<TEntity> entities);
    }

    /// <summary>
    /// Contrato asincrono de acceso a una entidad del dominio.
    /// </summary>
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>Elimina por llave primaria. Devuelve false si el registro no existe.</summary>
        Task<bool> DeleteAsync(params object[] keyValues);

        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
