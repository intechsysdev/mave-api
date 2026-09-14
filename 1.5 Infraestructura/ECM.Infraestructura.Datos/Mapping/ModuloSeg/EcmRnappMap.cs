using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRnappMap : IEntityTypeConfiguration<EcmRnapp>
    {
        public void Configure(EntityTypeBuilder<EcmRnapp> entity)
        {
            entity.HasKey(e => new { e.RnappEmprId, e.RnappUsuaCust, e.RnappUsuaSuccli, e.RnappMac, e.RnappDate }).HasName("PK_EcmRnapp");

            //entity.ToTable("ecm_rnapp", "informix");
            entity.ToTable("ecm_rnapp");

            entity.Property(e => e.RnappEmprId)
                .IsRequired()
                .HasColumnName("rnapp_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RnappUsuaCust)
                 .IsRequired()
                 .HasColumnName("rnapp_usua_cust")
                 .HasColumnType("varchar(15)");

            entity.Property(e => e.RnappUsuaSuccli)
                .IsRequired()
                .HasColumnName("rnapp_usua_succli")
                .HasColumnType("varchar(15)");

            entity.Property(e => e.RnappMac)
                .IsRequired()
                .HasColumnName("rnapp_mac")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RnappDate)
                .IsRequired()
                .HasColumnName("rnapp_date")
                .HasColumnType("date(4)");
            
            entity.Property(e => e.RnappApprove)
               .HasColumnName("rnapp_approve")
               .HasColumnType("char(1)");

            entity.Property(e => e.RnappVappId)
               .IsRequired()
               .HasColumnName("rnapp_vapp_id")
               .HasColumnType("int");

            entity.Property(e => e.RnappTappId)
               .IsRequired()
               .HasColumnName("rnapp_tapp_id")
               .HasColumnType("int");

            entity.Property(e => e.RnappNappId)
               .IsRequired()
               .HasColumnName("rnapp_napp_id")
               .HasColumnType("int");
        }
    }
}
