using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRordHeadCustMap : IEntityTypeConfiguration<MobRordHeadCust>
    {
        public void Configure(EntityTypeBuilder<MobRordHeadCust> entity)
        {
            entity.HasKey(e => new { e.OrhNum, e.OrhCust, e.OrhShip }).HasName("PK_MobRordHeadCust");

            //entity.ToTable("mob_rordheadcust", "etascon");
            entity.ToTable("mob_rordheadcust");

            entity.Property(e => e.OrhCmpy)
                .IsRequired()
                .HasColumnName("orh_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.OrhNum)
                .IsRequired()
                .HasColumnName("orh_num")
                .HasColumnType("int");

            entity.Property(e => e.OrhCust)
                .IsRequired()
                .HasColumnName("orh_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.OrhShip)
                .IsRequired()
                .HasColumnName("orh_ship")
                .HasColumnType("char(15)");

            entity.Property(e => e.OrhEntdt)
                .HasColumnName("orh_entdt")
                .HasColumnType("date(4)");

            entity.Property(e => e.OrhTime)
                .HasColumnName("orh_time")
                .HasColumnType("char(10)");

            entity.Property(e => e.OrhCom)
                .HasColumnName("orh_com")
                .HasColumnType("char(60)");

            entity.Property(e => e.OrhShpd)
                .HasColumnName("orh_shpd")
                .HasColumnType("date(4)");

            entity.Property(e => e.OrhPo)
              .HasColumnName("orh_po")
              .HasColumnType("char(20)");

            entity.Property(e => e.OrhTaxp)
              .HasColumnName("orh_taxp")
              .HasColumnType("decimal(10,4)");

            entity.Property(e => e.OrhSchrg)
              .HasColumnName("orh_schrg")
              .HasColumnType("decimal(10,4)");

            entity.Property(e => e.OrhToken)
              .HasColumnName("orh_token")
              .HasColumnType("char(50)");

            entity.Property(e => e.OrhNum2)
              .HasColumnName("orh_num2")
              .HasColumnType("int");

            entity.Property(e => e.OrhMarca)
              .HasColumnName("orh_marca")
              .HasColumnType("char(1)");

            entity.Property(e => e.OrhDate)
                .HasColumnName("orh_date")
                .HasColumnType("date(4)");
        }
    }
}
