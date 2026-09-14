using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRcustUserMap : IEntityTypeConfiguration<MobRcustUser>
    {
        public void Configure(EntityTypeBuilder<MobRcustUser> entity)
        {
            entity.HasKey(e => new { e.MrcuCmpy, e.MrcuCust, e.MrcuSuccli}).HasName("PK_MobRcustUser");

            //entity.ToTable("mob_rcustuser", "dls");
            entity.ToTable("mob_rcustuser");

            entity.Property(e => e.MrcuCmpy)
                .IsRequired()
                .HasColumnName("mrcu_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.MrcuCust)
                .IsRequired()
                .HasColumnName("mrcu_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.MrcuSuccli)
                .IsRequired()
                .HasColumnName("mrcu_succli")
                .HasColumnType("char(15)");

            entity.Property(e => e.MrcuName)
                .HasColumnName("mrcu_name")
                .HasColumnType("char(40)");

            entity.Property(e => e.MrcuPassword)
                .HasColumnName("mrcu_password")
                .HasColumnType("char(30)");

            entity.Property(e => e.MrcuAutorizado)
                .HasColumnName("mrcu_autorizado")
                .HasColumnType("char(1)");

            entity.Property(e => e.MrcuTele)
                .HasColumnName("mrcu_tele")
                .HasColumnType("char(20)");

            entity.Property(e => e.MrcuEmail)
                .HasColumnName("mrcu_email")
                .HasColumnType("char(60)");

            entity.Property(e => e.MrcuVrMinimo)
                .HasColumnName("mrcu_vrminimo")
                .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.MrcuVrMaximo)
                .HasColumnName("mrcu_vrmaximo")
                .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.MrcuSchrc)
                .HasColumnName("mrcu_schrc")
                .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.MrcuIva)
                .HasColumnName("mrcu_iva")
                .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.MrcuType)
              .HasColumnName("mrcu_type")
              .HasColumnType("char(3)");

            entity.Property(e => e.MrcuTipCliente)
              .HasColumnName("mrcu_tipcliente")
              .HasColumnType("char(1)");
            
            entity.Property(e => e.MrcuLevel)
                .HasColumnName("mrcu_level")
                .HasColumnType("char(1)");

            entity.Property(e => e.MrcuDiasEnt)
                .HasColumnName("mrcu_diasent")
                .HasColumnType("smallint");

            entity.Property(e => e.MrcuCodPer)
                .HasColumnName("mrcu_codper")
                .HasColumnType("char(10)");
        }
    }
}
