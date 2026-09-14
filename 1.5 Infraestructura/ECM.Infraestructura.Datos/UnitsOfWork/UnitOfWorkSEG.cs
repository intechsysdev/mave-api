using Itdear.Infraestructura.Datos.Core.UnitOfWork;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.EntityFrameworkCore;
using ECM.Dominio.UnitsOfWork;
using ECM.Dominio.ModuloSeg.Entities;
using ECM.Infraestructura.Datos.Mapping.ModuloSeg;

namespace ECM.Infraestructura.Datos.UnidadTrabajo
{
    public class UnitOfWorkSEG : UnitOfWorkBase, IUnitOfWorkSEG
    {
        public UnitOfWorkSEG(DbContextOptions<UnitOfWorkSEG> opcionesDBContext, IContextAccessor contextAccessor = null) : base(opcionesDBContext, contextAccessor)
        {
        }

        public virtual DbSet<EcmMcontacto> EcmMcontacto { get; set; }
        public virtual DbSet<EcmMempresa> EcmMempresa { get; set; }
        public virtual DbSet<EcmMnapp> EcmMnapp { get; set; }
        public virtual DbSet<EcmMnapp> EcmPais { get; set; }
        public virtual DbSet<EcmMtapp> EcmMtapp { get; set; }
        public virtual DbSet<EcmMusuario> EcmMusuario { get; set; }
        public virtual DbSet<EcmMvapp> EcmMvapp { get; set; }
        public virtual DbSet<EcmRacceso> EcmRacceso { get; set; }
        public virtual DbSet<EcmRlogusuario> EcmRlogUsuario { get; set; }
        public virtual DbSet<EcmRnapp> EcmRnapp { get; set; }
        public virtual DbSet<EcmRpassword> EcmRpassword { get; set; }
        public virtual DbSet<EcmRterminosuso> EcmRterminosuso { get; set; }
        public virtual DbSet<EcmRmac> EcmRmac { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EcmMcontactoMap());
            modelBuilder.ApplyConfiguration(new EcmMempresaMap());
            modelBuilder.ApplyConfiguration(new EcmMmonedaMap());
            modelBuilder.ApplyConfiguration(new EcmMnappMap());
            modelBuilder.ApplyConfiguration(new EcmPaisMap());
            modelBuilder.ApplyConfiguration(new EcmMtappMap());
            modelBuilder.ApplyConfiguration(new EcmMusuarioMap());
            modelBuilder.ApplyConfiguration(new EcmMvappMap());
            modelBuilder.ApplyConfiguration(new EcmRaccesoMap());
            modelBuilder.ApplyConfiguration(new EcmRlogusuarioMap());
            modelBuilder.ApplyConfiguration(new EcmRnappMap());
            modelBuilder.ApplyConfiguration(new EcmRpasswordMap());
            modelBuilder.ApplyConfiguration(new EcmRterminosusoMap());
            modelBuilder.ApplyConfiguration(new EcmRmacMap());
        }
    }
}