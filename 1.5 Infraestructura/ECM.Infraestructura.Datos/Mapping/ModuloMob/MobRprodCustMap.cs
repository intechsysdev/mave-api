using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRprodCustMap : IEntityTypeConfiguration<MobRprodCust>
    {
        public void Configure(EntityTypeBuilder<MobRprodCust> entity)
        {
            entity.HasKey(e => new { e.MrpcCmpy, e.MrpcCust, e.MrpcSuccli, e.MrpcId }).HasName("PK_MobRprodCust");

            //entity.ToTable("mob_rprodcust", "etascon");
            entity.ToTable("mob_rprodcust");

            entity.Property(e => e.MrpcCmpy)
                .IsRequired()
                .HasColumnName("mrpc_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.MrpcCust)
                .IsRequired()
                .HasColumnName("mrpc_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.MrpcSuccli)
                .IsRequired()
                .HasColumnName("mrpc_succli")
                .HasColumnType("char(15)");

            entity.Property(e => e.MrpcId)
                .HasColumnName("mrpc_id")
                .HasColumnType("char(21)");

            entity.Property(e => e.MrpcPrecio)
                .HasColumnName("mrpc_precio")
                .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.MrpcTax)
                .HasColumnName("mrpc_tax")
                .HasColumnType("char(1)");

            entity.Property(e => e.MrpcCantMax)
                .HasColumnName("mrpc_cantmax")
                .HasColumnType("int");

            entity.Property(e => e.MrpcEan)
                .HasColumnName("mrpc_ean")
                .HasColumnType("char(15)");

            entity.Property(e => e.MrpcUnidemPaq)
                .HasColumnName("mrpc_unidempaq")
                .HasColumnType("smallint");

            entity.HasOne(d => d.MobRproductos)
                .WithMany(p => p.MobRprodCust)
                .HasForeignKey(d => new { d.MrpcCmpy, d.MrpcId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobRprodCust_MobRproductos");           
        }
    }
}
