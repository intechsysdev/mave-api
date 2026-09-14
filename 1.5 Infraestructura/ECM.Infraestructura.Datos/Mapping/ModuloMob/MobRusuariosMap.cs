using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRusuariosMap : IEntityTypeConfiguration<MobRusuarios>
    {
        public void Configure(EntityTypeBuilder<MobRusuarios> entity)
        {
            entity.HasKey(e => new { e.RusuCmpy, e.RusuCodusu }).HasName("PK_MobRusuarios");

            //entity.ToTable("mob_rusuarios", "dls");
            entity.ToTable("mob_rusuarios");

            entity.Property(e => e.RusuCmpy)
                .IsRequired()
                .HasColumnName("rusu_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RusuCodusu)
                .IsRequired()
                .HasColumnName("rusu_codusu")
                .HasColumnType("char(10)");

            entity.Property(e => e.RusuPasswd)
                .HasColumnName("rusu_passwd")
                .HasColumnType("char(10)");

            entity.Property(e => e.RusuNombre)
                .HasColumnName("rusu_nombre")
                .HasColumnType("char(80)");

            entity.Property(e => e.RusuCodper)
                .HasColumnName("rusu_codper")
                .HasColumnType("char(10)");

            entity.Property(e => e.RusuCorreo)
                .HasColumnName("rusu_correo")
                .HasColumnType("char(70)");

            entity.Property(e => e.RusuReg)
                .HasColumnName("rusu_reg")
                .HasColumnType("char(5)");

            entity.Property(e => e.RusuTeri)
                .HasColumnName("rusu_teri")
                .HasColumnType("char(5)");

            entity.Property(e => e.RusuEnter)
                .HasColumnName("rusu_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.RusuDate)
                .HasColumnName("rusu_date")
                .HasColumnType("date(4)");
            
            entity.Property(e => e.RusuTime)
                .HasColumnName("rusu_time")
                .HasColumnType("char(10)");

            entity.Property(e => e.RusuEstadoupd)
                .HasColumnName("rusu_estadoupd")
                .HasColumnType("char(1)");

            entity.HasOne(d => d.MobRperfiles)
              .WithMany(p => p.MobRusuarios)
              .HasForeignKey(d => new { d.RusuCmpy, d.RusuCodper })
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MobRusuaurios_MobRperfiles");
        }
    }
}
