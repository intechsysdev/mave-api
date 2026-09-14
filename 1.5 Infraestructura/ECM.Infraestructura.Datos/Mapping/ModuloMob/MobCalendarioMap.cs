using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECM.Dominio.ModuloMob.Entities;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobCalendarioMap : IEntityTypeConfiguration<MobCalendario>
    {
        public void Configure(EntityTypeBuilder<MobCalendario> entity)
        {
            entity.HasKey(e => new { e.McaCmpy, e.McaAno, e.McaMes}).HasName("PK_MobCalendario");

            //entity.ToTable("mob_calendario", "etascon");
            entity.ToTable("mob_calendario");

            entity.Property(e => e.McaCmpy)
                .IsRequired()
                .HasColumnName("mca_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.McaAno)
                .HasColumnName("mca_ano")
                .HasColumnType("int");

            entity.Property(e => e.McaMes)
                .IsRequired()
                .HasColumnName("mca_mes")
                .HasColumnType("int");

            entity.Property(e => e.McaFecIni)
                .IsRequired()
              .HasColumnName("mca_fecini")
              .HasColumnType("date(4)");

            entity.Property(e => e.McaFecFin)
                .IsRequired()
              .HasColumnName("mca_fecfin")
              .HasColumnType("date(4)");

            entity.Property(e => e.McaFecEnt)
                .IsRequired()
              .HasColumnName("mca_fecent")
              .HasColumnType("date(4)");

            entity.Property(e => e.McaFecTem)
                .IsRequired()
              .HasColumnName("mca_fectem")
              .HasColumnType("date(4)");

            entity.Property(e => e.McaFecCier)
                .IsRequired()
              .HasColumnName("mca_feccier")
              .HasColumnType("date(4)");


        }
    }
}
