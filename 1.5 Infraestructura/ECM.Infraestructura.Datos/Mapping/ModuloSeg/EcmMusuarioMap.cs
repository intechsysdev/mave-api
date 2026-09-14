using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMusuarioMap : IEntityTypeConfiguration<EcmMusuario>
    {
        public void Configure(EntityTypeBuilder<EcmMusuario> entity)
        {
            entity.HasKey(e => new { e.MusuaEmprId, e.MusuaCust, e.MusuaSuccli}).HasName("PK_EcmMusuario");

            //entity.ToTable("ecm_musuario", "informix");
            entity.ToTable("ecm_musuario");

            entity.Property(e => e.MusuaEmprId)
                .IsRequired()
                .HasColumnName("musua_empr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.MusuaCust)
                .IsRequired()
               .HasColumnName("musua_cust")
               .HasColumnType("varchar(15)");

            entity.Property(e => e.MusuaSuccli)
                .IsRequired()
               .HasColumnName("musua_succli")
               .HasColumnType("varchar(15)");

            entity.Property(e => e.MusuaName)
               .HasColumnName("musua_name")
               .HasColumnType("varchar(40)");

            entity.Property(e => e.MusuaPassword)
               .HasColumnName("musua_password")
               .HasColumnType("varchar(30)");

            entity.Property(e => e.MusuaPhone)
            .HasColumnName("musua_phone")
            .HasColumnType("varchar(20)");

            entity.Property(e => e.MusuaMail)
            .HasColumnName("musua_mail")
            .HasColumnType("varchar(60)");

            entity.Property(e => e.MusuaApprove)
            .HasColumnName("musua_approve")
            .HasColumnType("char(1)");

            entity.Property(e => e.MusuaPassEncrip)
            .HasColumnName("musua_passencrip")
            .HasColumnType("varchar(50)");

            entity.Property(e => e.MusuaToken)
            .HasColumnName("musua_token")
            .HasColumnType("varchar(50)");

            entity.Property(e => e.MusuaExpirationToken)
            .HasColumnName("musua_expiration_token")
            .HasColumnType("date(10)");

        }
    }
}
