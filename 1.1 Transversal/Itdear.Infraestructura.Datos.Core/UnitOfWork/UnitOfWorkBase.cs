using System;
using System.Threading;
using System.Threading.Tasks;
using Itdear.Dominio.Core.UnitOfWork;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.EntityFrameworkCore;

namespace Itdear.Infraestructura.Datos.Core.UnitOfWork
{
    /// <summary>
    /// Base de las unidades de trabajo de la solucion.
    ///
    /// Es a la vez el <see cref="DbContext"/> de EF Core y la unidad de trabajo que ven
    /// las capas superiores, de manera que todos los repositorios construidos sobre la
    /// misma instancia comparten el rastreo de cambios y confirman en una sola operacion.
    /// </summary>
    public abstract class UnitOfWorkBase : DbContext, IUnitOfWorkAsync
    {
        /// <summary>
        /// Contexto de la peticion en curso. Es opcional porque las herramientas de
        /// diseno de EF instancian el contexto fuera de una peticion http.
        /// </summary>
        protected readonly IContextAccessor ContextAccessor;

        protected UnitOfWorkBase(DbContextOptions options, IContextAccessor contextAccessor = null)
            : base(options)
        {
            ContextAccessor = contextAccessor;
        }
    }
}
