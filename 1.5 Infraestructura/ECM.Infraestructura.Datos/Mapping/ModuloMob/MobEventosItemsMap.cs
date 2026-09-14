using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobEventosItemsMap : IEntityTypeConfiguration<MobEventosItems>
    {
        public void Configure(EntityTypeBuilder<MobEventosItems> entity)
        {
            entity.HasKey(e => new { e.EiCmpy, e.EiType, e.EiId }).HasName("PK_MobEventosItems");

            //entity.ToTable("mob_eventositems", "etascon");
            entity.ToTable("mob_eventositems");

            entity.Property(e => e.EiCmpy)
                .IsRequired()
                .HasColumnName("ei_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.EiType)
                .IsRequired()
                .HasColumnName("ei_type")
                .HasColumnType("char(10)");

            entity.Property(e => e.EiId)
                .IsRequired()
                .HasColumnName("ei_id")
                .HasColumnType("char(21)");

            entity.Property(e => e.EiDesc)
                .HasColumnName("ei_desc")
                .HasColumnType("char(40)");

            entity.Property(e => e.EiFecIni)
                .HasColumnName("ei_fecini")
                .HasColumnType("date(4)");

            entity.Property(e => e.EiFecFin)
                .HasColumnName("ei_fecfin")
                .HasColumnType("date(4)");

            entity.Property(e => e.EiEnter)
                .HasColumnName("ei_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.EiEntdt)
                .HasColumnName("ei_entdt")
                .HasColumnType("date(4)");

            entity.Property(e => e.EiTime)
                .HasColumnName("ei_time")
                .HasColumnType("char(10)");
        }
    }
}
