using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRcustConsecMap : IEntityTypeConfiguration<MobRcustConsec>
    {
        public void Configure(EntityTypeBuilder<MobRcustConsec> entity)
        {
            entity.HasKey(e => new { e.MdcCmpy, e.MdcCust, e.MdcSuccli, e.MdcTipdoc, e.MdcConsec}).HasName("PK_MobRcustConsec");

            //entity.ToTable("mob_rcustconsec", "etascon");
            entity.ToTable("mob_rcustconsec");

            entity.Property(e => e.MdcCmpy)
                .IsRequired()
                .HasColumnName("mdc_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.MdcCust)
                .IsRequired()
                .HasColumnName("mc_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.MdcSuccli)
                .IsRequired()
                .HasColumnName("mdc_succli")
                .HasColumnType("char(15)");

            entity.Property(e => e.MdcTipdoc)
                .IsRequired()
                .HasColumnName("mdc_tipdoc")
                .HasColumnType("char(1)");

            entity.Property(e => e.MdcConsec)
                .HasColumnName("mdc_consec")
                .HasColumnType("int");

            entity.Property(e => e.MdcEnter)
                .HasColumnName("mdc_enter")
                .HasColumnType("char(10)");

            entity.Property(e => e.MdcEntdt)
                .HasColumnName("mdc_entdt")
                .HasColumnType("date(4)");

            entity.Property(e => e.MdcTime)
                .HasColumnName("mdc_time")
                .HasColumnType("char(10)");            
        }
    }
}
