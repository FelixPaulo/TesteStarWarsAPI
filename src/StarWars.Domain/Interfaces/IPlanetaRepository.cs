using StarWars.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Domain.Interfaces
{
    public interface IPlanetaRepository : IRepository<Planeta>
    {
        Task<Planeta> ObterPlanetaPorNome(string nome);

        Task<Planeta> ObterPlanetaPorId(int id);

        Task<IEnumerable<Planeta>> ObterTodosPlanetas();
    }
}
