using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMmonedaMap : IEntityTypeConfiguration<EcmMmoneda>
    {
        public void Configure(EntityTypeBuilder<EcmMmoneda> entity)
        {
            entity.HasKey(e => new { e.MmoneId }).HasName("PK_EcmMmoneda");

            //entity.ToTable("ecm_mmoneda", "informix");
            entity.ToTable("ecm_mmoneda");

            entity.Property(e => e.MmoneId)
                .IsRequired()
                .HasColumnName("mmone_id")
                .HasColumnType("int");

            entity.Property(e => e.MmoneName)
               .HasColumnName("mmone_name")
               .HasColumnType("varchar(50)");
        }
    }
}
