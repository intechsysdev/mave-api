using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRordLineCustMap : IEntityTypeConfiguration<MobRordLineCust>
    {
        public void Configure(EntityTypeBuilder<MobRordLineCust> entity)
        {
            entity.HasKey(e => new { e.OrlNum, e.OrlCust, e.OrlShip, e.OrlPart }).HasName("PK_MobRordLineCust");

            //entity.ToTable("mob_rordlinecust", "etascon");
            entity.ToTable("mob_rordlinecust");

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

            entity.HasOne(d => d.MobRordHeadCust)
               .WithMany(p => p.MobRordLineCust)
               .HasForeignKey(d => new { d.OrlNum, d.OrlCust, d.OrlShip})
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_MobRordLineCust_MobRordHeadCust");

            entity.HasOne(d => d.MobRproductos)
              .WithMany(p => p.MobRordLineCust)
              .HasForeignKey(d => new { d.OrlCmpy, d.OrlPart })
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MobRordLineCust_MobRproductos");
        }
    }
}
