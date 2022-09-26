using Microsoft.EntityFrameworkCore;
using StarWars.Domain.Interfaces;
using StarWars.Domain.Models;
using StarWars.Infra.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Infra.Data.Repository
{
    public class PlanetaRepository : Repository<Planeta>, IPlanetaRepository
    {
        public PlanetaRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<Planeta> ObterPlanetaPorNome(string nome)
        {
            return await _dbSet.Include(x => x.Filmes).AsNoTracking().FirstOrDefaultAsync(x => x.Nome == nome);
        }

        public async Task<Planeta> ObterPlanetaPorId(int id)
        {
            return await _dbSet.Include(x => x.Filmes).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Planeta>> ObterTodosPlanetas()
        {
            return await _dbSet.Include(x => x.Filmes).ToListAsync();
        }
    }
}
