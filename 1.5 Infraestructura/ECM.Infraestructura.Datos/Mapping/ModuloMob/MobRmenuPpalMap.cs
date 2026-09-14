using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRmenuPpalMap : IEntityTypeConfiguration<MobRmenuPpal>
    {
        public void Configure(EntityTypeBuilder<MobRmenuPpal> entity)
        {

            entity.HasKey(e => new { e.RmenCmpy, e.RmenIdioma, e.RmenCodmen }).HasName("PK_MobRmenuPpal"); //.ForDb2IsClustered(false);

            //entity.ToTable("mob_rmenuppal", "dls");
            entity.ToTable("mob_rmenuppal");

            entity.Property(e => e.RmenCmpy)
                .IsRequired()
                .HasColumnName("rmen_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RmenIdioma)
                .IsRequired()
                .HasColumnName("rmen_idioma")
                .HasColumnType("smallint(2)");

            entity.Property(e => e.RmenCodmen)
                .IsRequired()
                .HasColumnName("rmen_codmen")
                .HasColumnType("char(2)");

            entity.Property(e => e.RmenAplicacion)
                .HasColumnName("rmen_aplicacion")
                .HasColumnType("char(80)");

            entity.Property(e => e.RmenCabecera)
                .HasColumnName("rmen_cabecera")
                .HasColumnType("smallint(2)");

            entity.Property(e => e.RmenDate)
                .HasColumnName("rmen_date")
                .HasColumnType("date(4)");

            entity.Property(e => e.RmenDesmen)
                .HasColumnName("rmen_desmen")
                .HasColumnType("char(50)");

            entity.Property(e => e.RmenEnter)
                .HasColumnName("rmen_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.RmenEstadoupd)
                .HasColumnName("rmen_estadoupd")
                .HasColumnType("char(1)");

            entity.Property(e => e.RmenTime)
                .HasColumnName("rmen_time")
                .HasColumnType("char(10)");
            
            entity.Property(e => e.RmenIcono)
                .HasColumnName("rmen_icono")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RmenAplicacionMovil)
                .HasColumnName("rmen_aplicacion_movil")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RmenIconoMovil)
                .HasColumnName("rmen_icono_movil")
                .HasColumnType("varchar(80)");
        }
    }
}
