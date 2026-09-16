using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Itdear.Dominio.Core.UnitOfWork
{
    /// <summary>
    /// Unidad de trabajo: agrupa los cambios de los repositorios que comparten el
    /// mismo contexto y los confirma en una sola operacion.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        /// <summary>Acceso al conjunto de una entidad para consultas que cruzan varias tablas.</summary>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    /// <summary>
    /// Unidad de trabajo con confirmacion asincrona.
    /// </summary>
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
