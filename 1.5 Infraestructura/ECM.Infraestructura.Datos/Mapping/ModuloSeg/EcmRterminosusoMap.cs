using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRterminosusoMap : IEntityTypeConfiguration<EcmRterminosuso>
    {
        public void Configure(EntityTypeBuilder<EcmRterminosuso> entity)
        {
            entity.HasKey(e => new { e.RtermEmprId, e.RtermUsuaCust, e.RtermUsuaSuccli, e.RtermMac }).HasName("PK_EcmRterminosuso");

            //entity.ToTable("ecm_rterminosuso", "informix");
            entity.ToTable("ecm_rterminosuso");

            entity.Property(e => e.RtermEmprId)
                .IsRequired()
                .HasColumnName("rterm_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RtermUsuaCust)
                 .IsRequired()
                 .HasColumnName("rterm_usua_cust")
                 .HasColumnType("varchar(15)");

            entity.Property(e => e.RtermUsuaSuccli)
                .IsRequired()
                .HasColumnName("rterm_usua_succli")
                .HasColumnType("varchar(15)");

            entity.Property(e => e.RtermMac)
                .IsRequired()
                .HasColumnName("rterm_mac")
                .HasColumnType("varchar(80)");

            entity.Property(e => e.RtermApprove)
                .IsRequired()
                .HasColumnName("rterm_approve")
                .HasColumnType("char(1)");
            
            entity.Property(e => e.RtermDate)
               .IsRequired()
               .HasColumnName("rterm_date")
               .HasColumnType("date(4)");
        }
    }
}
