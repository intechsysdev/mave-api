using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMtappMap : IEntityTypeConfiguration<EcmMtapp>
    {
        public void Configure(EntityTypeBuilder<EcmMtapp> entity)
        {
            entity.HasKey(e => new { e.RtappId }).HasName("PK_EcmMtapp");

            //entity.ToTable("ecm_mtapp", "informix");
            entity.ToTable("ecm_mtapp");

            entity.Property(e => e.RtappId)
                .IsRequired()
                .HasColumnName("rtapp_id")
                .HasColumnType("int");

            entity.Property(e => e.RvappName)
               .HasColumnName("rvapp_name")
               .HasColumnType("varchar(100)");
        }
    }
}
