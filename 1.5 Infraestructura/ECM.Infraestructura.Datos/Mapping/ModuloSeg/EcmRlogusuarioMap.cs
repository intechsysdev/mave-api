using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRlogusuarioMap : IEntityTypeConfiguration<EcmRlogusuario>
    {
        public void Configure(EntityTypeBuilder<EcmRlogusuario> entity)
        {
            entity.HasKey(e => new { e.RlogussEmprId, e.RlogusUsuaCust, e.RlogusUsuaSuccli, e.RlogusMac, e.RlogusDate }).HasName("PK_EcmRlogusuario");

            //entity.ToTable("ecm_rlogusuario", "informix");
            entity.ToTable("ecm_rlogusuario");

            entity.Property(e => e.RlogussEmprId)
                .IsRequired()
                .HasColumnName("rloguss_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RlogusUsuaCust)
                 .IsRequired()
                 .HasColumnName("rlogus_usua_cust")
                 .HasColumnType("varchar(15)");

            entity.Property(e => e.RlogusUsuaSuccli)
                .IsRequired()
                .HasColumnName("rlogus_usua_succli")
                .HasColumnType("varchar(15)");

            entity.Property(e => e.RlogusMac)
                .IsRequired()
                .HasColumnName("rlogus_mac")
                .HasColumnType("varchar(20)");

            entity.Property(e => e.RlogusDate)
                .HasColumnName("rlogus_date")
                .HasColumnType("date(4)");
            
            entity.Property(e => e.RlogusData)
               .HasColumnName("rlogus_data")
               .HasColumnType("varchar(100)");

            entity.Property(e => e.RlogusValue)
               .HasColumnName("rlogus_value")
               .HasColumnType("varchar(100)");

            entity.Property(e => e.RlogusDescription)
               .HasColumnName("rlogus_description")
               .HasColumnType("varchar(200)");
        }
    }
}
