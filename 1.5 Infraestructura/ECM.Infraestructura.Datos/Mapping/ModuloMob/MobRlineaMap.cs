using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRlineaMap : IEntityTypeConfiguration<MobRlinea>
    {
        public void Configure(EntityTypeBuilder<MobRlinea> entity)
        {
            entity.HasKey(e => new { e.RlnCmpy, e.RlnLinea}).HasName("PK_MobRlinea");

            //entity.ToTable("mob_rlinea", "dls");
            entity.ToTable("mob_rlinea");

            entity.Property(e => e.RlnCmpy)
                .IsRequired()
                .HasColumnName("rln_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RlnLinea)
                .IsRequired()
                .HasColumnName("rln_linea")
                .HasColumnType("char(4)");

            entity.Property(e => e.RlnDescr)
                .HasColumnName("rln_descr")
                .HasColumnType("char(40)");
        }
    }
}
