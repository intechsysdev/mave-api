using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRmacMap : IEntityTypeConfiguration<EcmRmac>
    {
        public void Configure(EntityTypeBuilder<EcmRmac> entity)
        {
            entity.HasKey(e => new { e.RmacEmprId, e.RmacUsuaCust, e.RmacUsuaSuccli, e.RmacMac }).HasName("PK_EcmRmac");

            //entity.ToTable("ecm_rmac", "informix");
            entity.ToTable("ecm_rmac");

            entity.Property(e => e.RmacEmprId)
                .IsRequired()
                .HasColumnName("rmac_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RmacUsuaCust)
                .IsRequired()
               .HasColumnName("rmac_usua_cust")
               .HasColumnType("varchar(15)");

            entity.Property(e => e.RmacUsuaSuccli)
               .HasColumnName("rmac_usua_succli")
               .HasColumnType("varchar(15)");

            entity.Property(e => e.RmacMac)
               .HasColumnName("rmac_mac")
               .HasColumnType("varchar(20)");

            entity.Property(e => e.RmacDate)
               .HasColumnName("rmac_date")
               .HasColumnType("date(4)");

            entity.Property(e => e.RmacVapp2fa)
               .HasColumnName("rmac_vapp_2fa")
               .HasColumnType("varchar(50)");
        }
    }
}
