using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWars.Domain.Models;

namespace StarWars.Infra.Data.Mappings
{
    public class PlanetaMapping : IEntityTypeConfiguration<Planeta>
    {
        public void Configure(EntityTypeBuilder<Planeta> builder)
        {
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.RuleLevelCascadeMode);
            builder.Ignore(e => e.ClassLevelCascadeMode);
            builder.Ignore(e => e.CascadeMode);

            builder
                .ToTable("Planeta")
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
                .Property(c => c.Terreno)
                .HasColumnName("Email")
                .HasMaxLength(200)
                .IsRequired();

            builder
              .Property(c => c.Clima)
              .HasColumnName("Clima")
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
