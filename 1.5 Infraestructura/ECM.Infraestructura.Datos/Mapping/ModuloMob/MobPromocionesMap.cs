using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobPromocionesMap : IEntityTypeConfiguration<MobPromociones>
    {
        public void Configure(EntityTypeBuilder<MobPromociones> entity)
        {
            entity.HasKey(e => new { e.PrmCmpy, e.PrmFecIni, e.PrmFecFin, e.PrmUrlbanner, e.PrmBanner, e.PrmEnter, e.PrmDate, e.PrmTime }).HasName("PK_MobPromociones");

            //entity.ToTable("mob_promociones", "dls");
            entity.ToTable("mob_promociones");

            entity.Property(e => e.PrmCmpy)
                .IsRequired()
                .HasColumnName("prm_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.PrmFecIni)
                .HasColumnName("prm_fecini")
                .HasColumnType("date(4)");

            entity.Property(e => e.PrmFecFin)
                .HasColumnName("prm_fecfin")
                .HasColumnType("date(4)");

            entity.Property(e => e.PrmUrlbanner)
                .HasColumnName("prm_urlbanner")
                .HasColumnType("char(200)");

            entity.Property(e => e.PrmBanner)
                .HasColumnName("prm_banner")
                .HasColumnType("char(100)");

            entity.Property(e => e.PrmEnter)
                .HasColumnName("prm_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.PrmDate)
                .HasColumnName("prm_date")
                .HasColumnType("date(4)");

            entity.Property(e => e.PrmTime)
                .HasColumnName("prm_time")
                .HasColumnType("char(10)");
        }
    }
}
