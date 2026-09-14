using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRperfilesMap : IEntityTypeConfiguration<MobRperfiles>
    {
        public void Configure(EntityTypeBuilder<MobRperfiles> entity)
        {
            entity.HasKey(e => new { e.RperCmpy, e.RperCodPer }).HasName("PK_MobRperfiles");

            //entity.ToTable("mob_rperfiles", "dls");
            entity.ToTable("mob_rperfiles");

            entity.Property(e => e.RperCmpy)
                .IsRequired()
                .HasColumnName("rper_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RperCodPer)
                .IsRequired()
                .HasColumnName("rper_codper")
                .HasColumnType("char(10)");

            entity.Property(e => e.RperDesPer)
                .HasColumnName("rper_desper")
                .HasColumnType("char(40)");           
        }
    }
}
