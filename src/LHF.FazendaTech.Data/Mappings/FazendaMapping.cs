using LHF.FazendaTech.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LHF.FazendaTech.Data.Mappings
{
    public class FazendaMapping : IEntityTypeConfiguration<Fazenda>
    {
        public void Configure(EntityTypeBuilder<Fazenda> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
               .IsRequired()
               .HasColumnType("varchar(14)");

            builder.Property(f => f.TipoProprietario)
                   .IsRequired()
                   .HasColumnType("Int16");

            // 1 : 1 => Endereco : Fazenda
            builder.HasOne(e => e.Endereco)
                .WithOne(f => f.Fazenda);

            builder.ToTable("Fazendas");
        }
    }
}
