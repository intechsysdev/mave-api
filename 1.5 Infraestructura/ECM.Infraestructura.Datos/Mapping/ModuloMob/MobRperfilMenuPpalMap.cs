using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRperfilMenuPpalMap : IEntityTypeConfiguration<MobRperfilMenuPpal>
    {
        public void Configure(EntityTypeBuilder<MobRperfilMenuPpal> entity)
        {
            entity.HasKey(e => new { e.RpfmCmpy, e.RpfmCodper, e.RpfmCodmen, e.RpfmCodsubmen }).HasName("PK_MobRperfilMenuPpal");
            //.ForDb2IsClustered(false);

            //entity.ToTable("mob_rperfilmenuppal", "dls");
            entity.ToTable("mob_rperfilmenuppal");

            entity.Property(e => e.RpfmCmpy)
                .IsRequired()
                .HasColumnName("rpfm_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RpfmCodper)
                .IsRequired()
                .HasColumnName("rpfm_codper")
                .HasColumnType("char(10)");

            entity.Property(e => e.RpfmCodmen)
                .IsRequired()
                .HasColumnName("rpfm_codmen")
                .HasColumnType("char(2)");

            entity.Property(e => e.RpfmCodsubmen)
                .IsRequired()
                .HasColumnName("rpfm_codsubmen")
                .HasColumnType("char(2)");

            entity.Property(e => e.RpfmDate)
                .HasColumnName("rpfm_date")
                .HasColumnType("date(4)");

            entity.Property(e => e.RpfmEnter)
                .HasColumnName("rpfm_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.RpfmEstadoupd)
                .HasColumnName("rpfm_estadoupd")
                .HasColumnType("char(1)");

            entity.Property(e => e.RpfmTime)
                .HasColumnName("rpfm_time")
                .HasColumnType("char(10)");

            entity.HasOne(d => d.MobRperfiles)
              .WithMany(p => p.MobRperfilMenuPpal)
              .HasForeignKey(d => new { d.RpfmCmpy, d.RpfmCodper })
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MobRperfilMenuPpal_MobRperfiles");

            //entity.HasOne(d => d.MobRsubMenu)
            // .WithMany(p => p.MobRperfilMenuPpal)
            // .HasForeignKey(d => new { d.RpfmCmpy, d.RpfmCodmen, d.RpfmCodsubmen })
            // .OnDelete(DeleteBehavior.ClientSetNull)
            // .HasConstraintName("FK_MobRperfilMenuPpal_MobRsubMenu");
        }
    }
}
