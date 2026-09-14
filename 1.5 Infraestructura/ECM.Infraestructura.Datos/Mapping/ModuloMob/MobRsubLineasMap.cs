using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRsubLineasMap : IEntityTypeConfiguration<MobRsubLineas>
    {
        public void Configure(EntityTypeBuilder<MobRsubLineas> entity)
        {
            entity.HasKey(e => new { e.RsbCmpy, e.RsbLinea, e.RsbSubl}).HasName("PK_MobRsubLineas");

            //entity.ToTable("mob_rsublineas", "dls");
            entity.ToTable("mob_rsublineas");

            entity.Property(e => e.RsbCmpy)
                .IsRequired()
                .HasColumnName("rsb_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RsbLinea)
                .IsRequired()
                .HasColumnName("rsb_linea")
                .HasColumnType("char(4)");

            entity.Property(e => e.RsbSubl)
                .IsRequired()
                .HasColumnName("rsb_subl")
                .HasColumnType("char(6)");

            entity.Property(e => e.RsbDescr)
                .HasColumnName("rsb_descr")
                .HasColumnType("char(40)");

            entity.HasOne(d => d.MobRlinea)
                .WithMany(p => p.MobRsubLineas)
                .HasForeignKey(d => new { d.RsbCmpy, d.RsbLinea })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobRsubLineas_MobRLinea");
        }
    }
}
