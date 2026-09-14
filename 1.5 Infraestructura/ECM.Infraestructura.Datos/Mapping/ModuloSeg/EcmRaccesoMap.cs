using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRaccesoMap : IEntityTypeConfiguration<EcmRacceso>
    {
        public void Configure(EntityTypeBuilder<EcmRacceso> entity)
        {
            entity.HasKey(e => new { e.RacceEmprId, e.RacceUsuaCust, e.RacceUsuaSuccli, e.RacceMac, e.RacceDate }).HasName("PK_EcmRacceso");

            //entity.ToTable("ecm_racceso", "informix");
            entity.ToTable("ecm_racceso");

            entity.Property(e => e.RacceEmprId)
                .IsRequired()
                .HasColumnName("racce_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RacceUsuaCust)
                 .IsRequired()
                 .HasColumnName("racce_usua_cust")
                 .HasColumnType("varchar(15)");

            entity.Property(e => e.RacceUsuaSuccli)
                .IsRequired()
                .HasColumnName("racce_usua_succli")
                .HasColumnType("varchar(15)");

            entity.Property(e => e.RacceMac)
                .IsRequired()
                .HasColumnName("racce_mac")
                .HasColumnType("varchar(20)");

            entity.Property(e => e.RacceDate)
                .IsRequired()
                .HasColumnName("racce_date")
                .HasColumnType("date(4)");
            
            entity.Property(e => e.RacceVappId)
               .IsRequired()
               .HasColumnName("racce_vapp_id")
               .HasColumnType("int");

            entity.Property(e => e.RacceTappId)
               .IsRequired()
               .HasColumnName("racce_tapp_id")
               .HasColumnType("int");
        }
    }
}
