using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Application.Models
{
    public class PlanetaDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Clima { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Terreno { get; set; }

        public IEnumerable<FilmesDto> Filmes { get; set; }
    }
}
