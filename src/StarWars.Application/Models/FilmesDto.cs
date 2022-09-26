using System;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Application.Models
{
    public class FilmesDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Diretor { get; set; }
        public DateTime DataLancamento { get; set; }

    }
}
