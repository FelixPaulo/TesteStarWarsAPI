using StarWars.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Interfaces
{
    public interface ICadastroPlanetaApplication
    {
        Task<PlanetaResultDto> AddPlaneta(PlanetaDto planetaDto);
        Task<PlanetaResultDto> BuscaPlanetaPorNome(string nome);
        Task<PlanetaResultDto> BuscaPlanetaPorId(int id);
        Task<IEnumerable<PlanetaResultDto>> ListarTodosPlanetas();
        Task<bool> DeletarPlaneta(int id);
    }
}
