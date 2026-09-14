using Itdear.Infraestructura.Datos.Core.UnitOfWork;
using Itdear.Infraestructura.Transversal.ContextAccessor;
using Microsoft.EntityFrameworkCore;
using ECM.Dominio.ModuloMob.Entities;
using ECM.Dominio.UnitsOfWork;
using ECM.Infraestructura.Datos.Mapping.ModuloMob;

namespace ECM.Infraestructura.Datos.UnidadTrabajo
{
    public class UnitOfWorkECM : UnitOfWorkBase, IUnitOfWorkECM
    {
        public UnitOfWorkECM(DbContextOptions<UnitOfWorkECM> opcionesDBContext, IContextAccessor contextAccessor = null) : base(opcionesDBContext, contextAccessor)
        {
        }

        public virtual DbSet<MobCartera> MobCartera { get; set; }

        public virtual DbSet<MobCalendario> MobCalendario { get; set; }
        public virtual DbSet<MobEventosItems> MobEventosItems { get; set; }
        public virtual DbSet<MobPromociones> MobPromociones { get; set; }
        public virtual DbSet<MobRcustConsec> MobRcustConsec { get; set; }
        public virtual DbSet<MobRcustUser> MobRcustUser { get; set; }
        public virtual DbSet<MobRlinea> MobRlinea { get; set; }
        public virtual DbSet<MobRmenuPpal> MobRmenuPpal { get; set; }
        public virtual DbSet<MobRordHeadCust> MobRordHeadCust { get; set; }
        public virtual DbSet<MobRordLineCust> MobRordLineCust { get; set; }
        public virtual DbSet<MobRordLineCustShop> MobRordLineCustShop { get; set; }
        public virtual DbSet<MobRperfiles> MobRperfiles { get; set; }
        public virtual DbSet<MobRperfilMenuPpal> MobRperfilMenuPpal { get; set; }
        public virtual DbSet<MobRprodCust> MobRprodCust { get; set; }
        public virtual DbSet<MobRproductos> MobRproductos { get; set; }
        public virtual DbSet<MobRsubLineas> MobRsubLineas { get; set; }
        public virtual DbSet<MobRsubMenu> MobRsubMenu { get; set; }
        public virtual DbSet<MobRusuarios> MobRusuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MobCarteraMap());

            modelBuilder.ApplyConfiguration(new MobCalendarioMap());

            modelBuilder.ApplyConfiguration(new MobEventosItemsMap());

            modelBuilder.ApplyConfiguration(new MobPromocionesMap());

            modelBuilder.ApplyConfiguration(new MobRcustConsecMap());

            modelBuilder.ApplyConfiguration(new MobRcustUserMap());

            modelBuilder.ApplyConfiguration(new MobRlineaMap());

            modelBuilder.ApplyConfiguration(new MobRmenuPpalMap());

            modelBuilder.ApplyConfiguration(new MobRordHeadCustMap());

            modelBuilder.ApplyConfiguration(new MobRordLineCustMap());

            modelBuilder.ApplyConfiguration(new MobRordLineCustShopMap());

            modelBuilder.ApplyConfiguration(new MobRperfilesMap());

            modelBuilder.ApplyConfiguration(new MobRperfilMenuPpalMap());

            modelBuilder.ApplyConfiguration(new MobRprodCustMap());

            modelBuilder.ApplyConfiguration(new MobRproductosMap());

            modelBuilder.ApplyConfiguration(new MobRsubLineasMap());

            modelBuilder.ApplyConfiguration(new MobRsubMenuMap());

            modelBuilder.ApplyConfiguration(new MobRusuariosMap());
        }
    }
}