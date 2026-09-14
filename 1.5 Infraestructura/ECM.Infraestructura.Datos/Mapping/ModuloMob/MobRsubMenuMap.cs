using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRsubMenuMap : IEntityTypeConfiguration<MobRsubMenu>
    {
        public void Configure(EntityTypeBuilder<MobRsubMenu> entity)
        {
            entity.HasKey(e => new { e.RsubmCmpy, e.RsubmIdioma, e.RsubmCodmen, e.RsubmCodsubmen }).HasName("PK_MobRsubMenu");
            //.ForDb2IsClustered(false);

            //entity.ToTable("mob_rsubmenu", "dls");
            entity.ToTable("mob_rsubmenu");

            entity.Property(e => e.RsubmCmpy)
                .IsRequired()
                .HasColumnName("rsubm_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RsubmIdioma)
                .IsRequired()
                .HasColumnName("rsubm_idioma")
                .HasColumnType("smallint(2)");

            entity.Property(e => e.RsubmCodmen)
                .IsRequired()
                .HasColumnName("rsubm_codmen")
                .HasColumnType("char(2)");

            entity.Property(e => e.RsubmCodsubmen)
                .IsRequired()
                .HasColumnName("rsubm_codsubmen")
                .HasColumnType("char(2)");

            entity.Property(e => e.RsubmAplicacion)
                .HasColumnName("rsubm_aplicacion")
                .HasColumnType("char(80)");

            entity.Property(e => e.RsubmCabecera)
                .HasColumnName("rsubm_cabecera")
                .HasColumnType("smallint(2)");

            entity.Property(e => e.RsubmDate)
                .HasColumnName("rsubm_date")
                .HasColumnType("date(4)");

            entity.Property(e => e.RsubmDessubmen)
                .HasColumnName("rsubm_dessubmen")
                .HasColumnType("char(50)");

            entity.Property(e => e.RsubmEnter)
                .HasColumnName("rsubm_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.RsubmEstadoupd)
                .HasColumnName("rsubm_estadoupd")
                .HasColumnType("char(1)");

            entity.Property(e => e.RsubmTime)
                .HasColumnName("rsubm_time")
                .HasColumnType("char(10)");

            entity.Property(e => e.RsubmIcono)
                .HasColumnName("subm_icono")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RsubmAplicacionMovil)
                .HasColumnName("rsubm_aplicacion_movil")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RsubmIconoMovil)
                .HasColumnName("rsubm_icono_movil")
                .HasColumnType("varchar(80)");

            entity.HasOne(d => d.MobRmenuPpal)
                .WithMany(p => p.MobRsubMenu)
                .HasForeignKey(d => new { d.RsubmCmpy, d.RsubmIdioma, d.RsubmCodmen })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobRsubMenu_MobRmenuPpal");
        }
    }
}
