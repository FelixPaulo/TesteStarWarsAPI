using StarWars.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StarWars.Application.Models
{
    public class PlanetaResultDto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Clima { get; set; }
        public string Terreno { get; set; }

        public IEnumerable<FilmesResultDto> Filmes { get; set; }
    }

    public static class PlanetaResultDtoExtensions
    {
        /// <summary>
        /// Optei por fazer o map manualmente e não fazer uso do AutoMapper para não ter que adicionar mais uma dependencia no projeto
        /// </summary>
        /// <param name="planeta"></param>
        /// <returns></returns>
        public static PlanetaResultDto MapToModel(this Planeta planeta)
        {
            var planetaResultDto = new PlanetaResultDto
            {
                Id = planeta.Id,
                Nome = planeta.Nome,
                Clima = planeta.Clima,
                Terreno = planeta.Terreno,
                Filmes = planeta.Filmes != null && planeta.Filmes.Any() ? planeta.Filmes.MapToModels() : new List<FilmesResultDto>(),
            };

            return planetaResultDto;
        }

        public static IEnumerable<PlanetaResultDto> MapToModels(this IEnumerable<Planeta> planetas)
        {
            var lstPlanetasDtos = new List<PlanetaResultDto>();
            foreach (var planeta in planetas)
                lstPlanetasDtos.Add(planeta.MapToModel());
            return lstPlanetasDtos;
        }
    }
}
