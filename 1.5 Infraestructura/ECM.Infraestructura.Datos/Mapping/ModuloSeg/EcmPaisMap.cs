using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmPaisMap : IEntityTypeConfiguration<EcmPais>
    {
        public void Configure(EntityTypeBuilder<EcmPais> entity)
        {
            entity.HasKey(e => new { e.MPaisId }).HasName("PK_EcmPais");

            //entity.ToTable("ecm_pais", "informix");
            entity.ToTable("ecm_pais");

            entity.Property(e => e.MPaisId)
                .IsRequired()
                .HasColumnName("mpais_id")
                .HasColumnType("int");

            entity.Property(e => e.MPaisName)
               .HasColumnName("mpais_name")
               .HasColumnType("varchar(80)");
        }
    }
}
