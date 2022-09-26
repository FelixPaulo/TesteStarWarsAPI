using StarWars.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarWars.Application.Models
{
    public class FilmesResultDto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public DateTime DataLancamento { get; set; }
    }

    public static class FilmesResultDtoExtensions
    {
        private static FilmesResultDto MapToModel(this Filme filme)
        {
            var filmesResultDto = new FilmesResultDto
            {
                Id = filme.Id,
                Nome = filme.Nome,
                Diretor = filme.Diretor,
                DataLancamento = filme.DataLancamento,
            };
            return filmesResultDto;
        }

        public static IEnumerable<FilmesResultDto> MapToModels(this IEnumerable<Filme> filmes)
        {
            var lstFilmesDtos = new List<FilmesResultDto>();
            foreach (var filme in filmes) 
               lstFilmesDtos.Add(filme.MapToModel());
            return lstFilmesDtos;
        }
    }
}
