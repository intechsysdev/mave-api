using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRordLineCustShopMap : IEntityTypeConfiguration<MobRordLineCustShop>
    {
        public void Configure(EntityTypeBuilder<MobRordLineCustShop> entity)
        {
            entity.HasKey(e => new { e.OrlNum, e.OrlCust, e.OrlShip, e.OrlPart }).HasName("PK_MobRordLineCustShop");

            //entity.ToTable("mob_rordlinecust_shop", "informix");
            entity.ToTable("mob_rordlinecust_shop");

            entity.Property(e => e.OrlCmpy)
                .IsRequired()
                .HasColumnName("orl_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.OrlNum)
                .IsRequired()
                .HasColumnName("orl_num")
                .HasColumnType("int");

            entity.Property(e => e.OrlCust)
                .IsRequired()
                .HasColumnName("orl_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.OrlShip)
                .IsRequired()
                .HasColumnName("orl_ship")
                .HasColumnType("char(15)");

            entity.Property(e => e.OrlPart)
                .IsRequired()
                .HasColumnName("orl_part")
                .HasColumnType("char(21)");

            entity.Property(e => e.OrlEntdt)
                .HasColumnName("orl_entdt")
                .HasColumnType("date(4)");

            entity.Property(e => e.OrlTime)
                .HasColumnName("orl_time")
                .HasColumnType("char(10)");

            entity.Property(e => e.OrlQord)
                .HasColumnName("orl_qord")
                .HasColumnType("decimal(16, 4)");

            entity.Property(e => e.OrlTxbl)
              .HasColumnName("orl_txbl")
              .HasColumnType("char(1)");

            entity.Property(e => e.OrlLevel)
              .HasColumnName("orl_level")
              .HasColumnType("char(1)");

            entity.Property(e => e.OrlUnpr)
              .HasColumnName("orl_unpr")
              .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.OrlMarca)
              .HasColumnName("orl_marca")
              .HasColumnType("char(1)");

            entity.Property(e => e.OrlDate)
                .HasColumnName("orl_date")
                .HasColumnType("date(4)");

            entity.HasOne(d => d.MobRprodCust)
              .WithMany(p => p.MobRordLineCustShop)
              .HasForeignKey(d => new { d.OrlCmpy, d.OrlCust, d.OrlShip, d.OrlPart })
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MobRordLineCustShop_MobRprodCust");
        }
    }
}
