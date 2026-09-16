using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Itdear.Dominio.Core.Repositories;
using Itdear.Dominio.Core.Specifications;
using Itdear.Dominio.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Itdear.Infraestructura.Datos.Core.Repositories
{
    /// <summary>
    /// Repositorio generico sobre Entity Framework Core.
    ///
    /// Las operaciones de escritura solo marcan el estado de la entidad en el contexto;
    /// la confirmacion queda en manos de la unidad de trabajo, que es quien decide el
    /// alcance de la transaccion.
    /// </summary>
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWorkAsync _unitOfWork;

        protected readonly DbContext _context;

        protected readonly DbSet<TEntity> _dbSet;

        public Repository(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            _context = unitOfWork as DbContext
                ?? throw new ArgumentException(
                    "La unidad de trabajo debe derivar de DbContext para poder usarse como repositorio.",
                    nameof(unitOfWork));

            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Queryable() => _dbSet;

        public IQueryFluent<TEntity> Query() => new QueryFluent<TEntity>(_dbSet);

        public IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> criteria)
        {
            return new QueryFluent<TEntity>(criteria == null ? _dbSet : _dbSet.Where(criteria));
        }

        public IQueryFluent<TEntity> Query(ISpecification<TEntity> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return Query(specification.Criteria);
        }

        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public Task<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues).AsTask();

        public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            => _dbSet.FindAsync(keyValues, cancellationToken).AsTask();

        public void Insert(TEntity entity) => _dbSet.Add(entity);

        public void InsertRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public void Update(TEntity entity)
        {
            // Si la instancia ya viene rastreada por el contexto, Attach lanzaria por
            // llave duplicada: en ese caso basta con marcarla como modificada.
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public void Delete(params object[] keyValues)
        {
            var entity = Find(keyValues);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        public async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues).ConfigureAwait(false);

            if (entity == null)
            {
                return false;
            }

            Delete(entity);

            return true;
        }
    }
}
