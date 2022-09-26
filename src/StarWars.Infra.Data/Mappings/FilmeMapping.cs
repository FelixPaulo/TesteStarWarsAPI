using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWars.Domain.Models;

namespace StarWars.Infra.Data.Mappings
{
    public class FilmeMapping : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.RuleLevelCascadeMode);
            builder.Ignore(e => e.ClassLevelCascadeMode);
            builder.Ignore(e => e.CascadeMode);

            builder
                .ToTable("Filme")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName("Id");

            builder
                .Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(c => c.Diretor)
                .HasColumnName("Diretor")
                .HasMaxLength(300)
                .IsRequired();

            builder
               .Property(c => c.DataLancamento)
               .HasColumnName("DataLancamento")
               .IsRequired();

            builder
               .HasOne(p => p.Planeta)
               .WithMany(c => c.Filmes)
               .HasForeignKey(p => p.PlanetaId);
        }
    }
}
