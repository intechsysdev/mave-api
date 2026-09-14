using ECM.Dominio.ModuloMob.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloMob
{
    public class MobRproductosMap : IEntityTypeConfiguration<MobRproductos>
    {
        public void Configure(EntityTypeBuilder<MobRproductos> entity)
        {
            entity.HasKey(e => new { e.RprCmpy, e.RprId }).HasName("PK_MobRproductos");

            //entity.ToTable("mob_rproductos", "dls");
            entity.ToTable("mob_rproductos");

            entity.Property(e => e.RprCmpy)
                .IsRequired()
                .HasColumnName("rpr_cmpy")
                .HasColumnType("char(2)");

            entity.Property(e => e.RprId)
                .IsRequired()
                .HasColumnName("rpr_id")
                .HasColumnType("char(21)");

            entity.Property(e => e.RprDesc)
                .HasColumnName("rpr_desc")
                .HasColumnType("char(40)");

            entity.Property(e => e.RprLinea)
                .HasColumnName("rpr_linea")
                .HasColumnType("char(4)");

            entity.Property(e => e.RprSubl)
                .HasColumnName("rpr_subl")
                .HasColumnType("char(6)");

            entity.Property(e => e.RprNuevo)
                .HasColumnName("rpr_nuevo")
                .HasColumnType("smallint");

            entity.Property(e => e.RprComponentes)
                .HasColumnName("rpr_componentes")
                .HasColumnType("text");

            entity.Property(e => e.RprAplicacion)
                .HasColumnName("rpr_aplicacion")
                .HasColumnType("text");

            entity.Property(e => e.RprMediosPubli)
                .HasColumnName("rpr_mediospubli")
                .HasColumnType("text");

            entity.Property(e => e.RprUrlVideoClip)
                .HasColumnName("rpr_urlvideoclip")
                .HasColumnType("text");

            entity.Property(e => e.RprUrlImagen)
                .HasColumnName("rpr_urlimagen")
                .HasColumnType("char(100)");

            entity.Property(e => e.RprImagen)
                .HasColumnName("rpr_imagen")
                .HasColumnType("byte(2147483647)");

            entity.Property(e => e.RprEan)
                .HasColumnName("rpr_ean")
                .HasColumnType("char(20)");            

            entity.HasOne(d => d.MobRlinea)
                .WithMany(p => p.MobRproductos)
                .HasForeignKey(d => new { d.RprCmpy, d.RprLinea})
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobRproductos_MobRLinea");

            entity.HasOne(d => d.MobRsubLineas)
                .WithMany(p => p.MobRproductos)
                .HasForeignKey(d => new { d.RprCmpy, d.RprLinea, d.RprSubl })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobRproductos_MobRsubLineas");
          
        }
    }
}
