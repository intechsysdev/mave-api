using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMcontactoMap : IEntityTypeConfiguration<EcmMcontacto>
    {
        public void Configure(EntityTypeBuilder<EcmMcontacto> entity)
        {
            entity.HasKey(e => new { e.McontaId, e.McontEmprId }).HasName("PK_EcmMcontacto");

            //entity.ToTable("ecm_mcontacto", "informix");
            entity.ToTable("ecm_mcontacto");

            entity.Property(e => e.McontaId)
                .IsRequired()
                .HasColumnName("mcont_id")
                .HasColumnType("int");

            entity.Property(e => e.McontEmprId)
                .IsRequired()
               .HasColumnName("mcont_empr_id")
               .HasColumnType("char(2)");

            entity.Property(e => e.McontName)
               .HasColumnName("mcont_name")
               .HasColumnType("char(100)");

            entity.Property(e => e.McontMail)
               .HasColumnName("mcont_mail")
               .HasColumnType("char(100)");

            entity.Property(e => e.McontPhon)
               .HasColumnName("mcont_phon")
               .HasColumnType("char(20)");

            entity.Property(e => e.McontIden)
            .HasColumnName("mcont_iden")
            .HasColumnType("char(20)");
        }
    }
}
