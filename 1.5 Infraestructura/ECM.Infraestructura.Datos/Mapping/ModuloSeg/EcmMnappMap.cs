using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMnappMap : IEntityTypeConfiguration<EcmMnapp>
    {
        public void Configure(EntityTypeBuilder<EcmMnapp> entity)
        {
            entity.HasKey(e => new { e.MnappNappId, e.MnappVappId, e.MnappTappId }).HasName("PK_EcmMnapp");

            //entity.ToTable("ecm_mnapp", "informix");
            entity.ToTable("ecm_mnapp");

            entity.Property(e => e.MnappNappId)
                .IsRequired()
                .HasColumnName("mnapp_napp_id")
                .HasColumnType("int");

            entity.Property(e => e.MnappVappId)
               .HasColumnName("mnapp_vapp_id")
               .HasColumnType("int");

            entity.Property(e => e.MnappTappId)
               .HasColumnName("mnapp_tapp_id")
               .HasColumnType("int");

            entity.Property(e => e.MnappDescription)
               .HasColumnName("mnapp_description")
               .HasColumnType("varchar(200)");

            entity.Property(e => e.MnappDate)
               .HasColumnName("mnapp_date")
               .HasColumnType("date(4)");

            entity.Property(e => e.MnappImage)
            .HasColumnName("mnapp_image")
            .HasColumnType("varchar(255)");
        }
    }
}
