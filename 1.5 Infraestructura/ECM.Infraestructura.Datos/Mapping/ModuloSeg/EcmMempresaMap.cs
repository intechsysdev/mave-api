using ECM.Dominio.ModuloSeg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECM.Infraestructura.Datos.Mapping.ModuloSeg
{
    public class EcmMempresaMap : IEntityTypeConfiguration<EcmMempresa>
    {
        public void Configure(EntityTypeBuilder<EcmMempresa> entity)
        {
            entity.HasKey(e => new { e.MemprId }).HasName("PK_EcmMempresa");

            //entity.ToTable("ecm_mempresa", "informix");
            entity.ToTable("ecm_mempresa");

            entity.Property(e => e.MemprId)
                .IsRequired()
                .HasColumnName("mempr_id")
                .HasColumnType("varchar(2)");

            entity.Property(e => e.MemprePaisId)
               .HasColumnName("mempre_pais_id")
               .HasColumnType("int");

            entity.Property(e => e.MempreMoneId)
               .HasColumnName("mempre_mone_id")
               .HasColumnType("int");

            entity.Property(e => e.MemprIden)
               .HasColumnName("mempr_iden")
               .HasColumnType("varchar(20)");

            entity.Property(e => e.MemprCmpy)
               .HasColumnName("mempr_cmpy")
               .HasColumnType("varchar(2)");

            entity.Property(e => e.MemprName)
            .HasColumnName("mempr_name")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprCome)
            .HasColumnName("mempr_come")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprAddr)
            .HasColumnName("mempr_addr")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprPhon)
            .HasColumnName("mempr_phon")
            .HasColumnType("varchar(20)");

            entity.Property(e => e.MemprWeb)
            .HasColumnName("mempr_web")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprFdate)
            .HasColumnName("mempr_fdate")
            .HasColumnType("varchar(20)");

            entity.Property(e => e.MemprFcurr)
            .HasColumnName("mempr_fcurr")
            .HasColumnType("varchar(20)");

            entity.Property(e => e.MemprFnumb)
            .HasColumnName("mempr_fnumb")
            .HasColumnType("varchar(20)");

            entity.Property(e => e.MemprApisms)
            .HasColumnName("mempr_apisms")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprTerm)
            .HasColumnName("mempr_term")
            .HasColumnType("ntext");

            entity.Property(e => e.MemprData)
            .HasColumnName("mempr_data")
            .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprConn)
            .HasColumnName("mempr_conn")
            .HasColumnType("varchar(200)");

            entity.Property(e => e.MemprApiapp2fa)
           .HasColumnName("mempr_apiapp2fa")
           .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprApimen2fa)
           .HasColumnName("mempr_apimen2fa")
           .HasColumnType("varchar(100)");

            entity.Property(e => e.MemprNumhisord)
           .HasColumnName("mempr_numhisord")
           .HasColumnType("decimal(2)");            
        }
    }
}