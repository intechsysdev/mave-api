using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECM.Dominio.ModuloMob.Entities;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobCarteraMap : IEntityTypeConfiguration<MobCartera>
    {
        public void Configure(EntityTypeBuilder<MobCartera> entity)
        {
            entity.HasKey(e => new { e.CarCmpy, e.CarTipo, e.CarNumero }).HasName("PK_MobCartera");

            //entity.ToTable("mob_cartera", "dls");
            entity.ToTable("mob_cartera");

            entity.Property(e => e.CarCmpy)
                .IsRequired()
                .HasColumnName("car_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.CarCodusu)
                .HasColumnName("car_codusu")
                .HasColumnType("char(10)");

            entity.Property(e => e.CarTipo)
                .IsRequired()
                .HasColumnName("car_tipo")
                .HasColumnType("char(2)");

            entity.Property(e => e.CarNumero)
                .IsRequired()
              .HasColumnName("car_numero")
              .HasColumnType("decimal(10, 2)");

            entity.Property(e => e.CarCust)
                .HasColumnName("car_cust")
                .HasColumnType("char(15)");

            entity.Property(e => e.CarSuccli)
                .HasColumnName("car_succli")
                .HasColumnType("char(15)");

            entity.Property(e => e.CarGuia)
                .HasColumnName("car_guia")
                .HasColumnType("char(15)");

            entity.Property(e => e.CarFecha)
                .HasColumnName("car_fecha")
                .HasColumnType("date(4)");

            entity.Property(e => e.CarDiasven)
              .HasColumnName("car_diasven")
              .HasColumnType("decimal(10, 2)");

            entity.Property(e => e.CarAmt)
              .HasColumnName("car_amt")
              .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.CarSaldo)
              .HasColumnName("car_saldo")
              .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.CarNotacredito)
              .HasColumnName("car_notacredito")
              .HasColumnType("decimal(16, 2)");

            entity.Property(e => e.CarAplicanc)
                .HasColumnName("car_aplicanc")
                .HasColumnType("char(1)");

            entity.Property(e => e.CarFechaven)
                .HasColumnName("car_fechaven")
                .HasColumnType("date(4)");           
        }
    }
}
