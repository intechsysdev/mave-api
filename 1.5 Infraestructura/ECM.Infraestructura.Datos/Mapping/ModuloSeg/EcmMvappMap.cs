using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMvappMap : IEntityTypeConfiguration<EcmMvapp>
    {
        public void Configure(EntityTypeBuilder<EcmMvapp> entity)
        {
            entity.HasKey(e => new { e.RvappId, e.RvappTappId }).HasName("PK_EcmMvapp");

            //entity.ToTable("ecm_mvapp", "informix");
            entity.ToTable("ecm_mvapp");

            entity.Property(e => e.RvappId)
                .IsRequired()
                .HasColumnName("rvapp_id")
                .HasColumnType("int");

            entity.Property(e => e.RvappTappId)
                .IsRequired()
               .HasColumnName("rvapp_tapp_id")
               .HasColumnType("int");

            entity.Property(e => e.RvappDescription)
               .HasColumnName("rvapp_description")
               .HasColumnType("char(100)");

            entity.Property(e => e.RvappDate)
               .HasColumnName("rvapp_date")
               .HasColumnType("date(4)");

            entity.Property(e => e.RvappVersion)
               .HasColumnName("rvapp_version")
               .HasColumnType("varchar(20)");

            entity.Property(e => e.RvappToken)
               .HasColumnName("rvapp_token")
               .HasColumnType("varchar(100)");
        }
    }
}
