using Microsoft.EntityFrameworkCore;
using StarWars.Domain.Models;
using StarWars.Infra.Data.Mappings;

namespace StarWars.Infra.Data.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Planeta> Planetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlanetaMapping());
            modelBuilder.ApplyConfiguration(new FilmeMapping());
        }

        public virtual int SaveChanges(string userName, int companyId)
        {
            var result = SaveChanges();
            return result;
        }
    }
}
