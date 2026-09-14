using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmRpasswordMap : IEntityTypeConfiguration<EcmRpassword>
    {
        public void Configure(EntityTypeBuilder<EcmRpassword> entity)
        {
            entity.HasKey(e => new { e.RpassEmprId, e.RpassUsuaCust, e.RpassUsuaSuccli, e.RpassDate }).HasName("PK_EcmRpassword");

            //entity.ToTable("ecm_rpassword", "informix");
            entity.ToTable("ecm_rpassword");

            entity.Property(e => e.RpassEmprId)
                .IsRequired()
                .HasColumnName("rpass_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.RpassUsuaCust)
                 .IsRequired()
                 .HasColumnName("rpass_usua_cust")
                 .HasColumnType("varchar(15)");

            entity.Property(e => e.RpassUsuaSuccli)
                .IsRequired()
                .HasColumnName("rpass_usua_succli")
                .HasColumnType("varchar(15)");

            entity.Property(e => e.RpassDate)
                .IsRequired()
                .HasColumnName("rpass_date")
                .HasColumnType("date(4)");

            entity.Property(e => e.RpassPassword)
                .IsRequired()
                .HasColumnName("rpass_password")
                .HasColumnType("varchar(30)");
            
            entity.Property(e => e.RpassCode)
               .HasColumnName("rpass_code")
               .HasColumnType("varchar(10)");
            
        }
    }
}
