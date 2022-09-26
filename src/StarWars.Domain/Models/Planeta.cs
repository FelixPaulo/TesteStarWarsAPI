using FluentValidation;
using StarWars.Domain.Core;
using System;
using System.Collections.Generic;

namespace StarWars.Domain.Models
{
    public class Planeta : Entity<Planeta>
    {
        public Planeta() { }

        public Planeta(string nome, string clima, string terreno)
        {
            Nome = nome;
            Clima = clima;
            Terreno = terreno;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Clima { get; private set; }
        public string Terreno { get; private set; }

        public virtual ICollection<Filme> Filmes { get; private set; }

        public void SetFilmes(string nome, string diretor, DateTime dataLancamento)
        {
            if(Filmes == null)
                Filmes = new List<Filme>();

            Filmes.Add(new Filme(nome, diretor, dataLancamento));
        }

        public override bool IsValid()
        {

            RuleFor(c => c.Nome)
              .NotEmpty().WithMessage("O nome é obrigatório")
              .MaximumLength(200).WithMessage("O tamanho maximo do nome é 200");
            RuleFor(c => c.Clima)
               .NotEmpty().WithMessage("O clima é obrigatório");
            RuleFor(c => c.Terreno)
            .NotEmpty().WithMessage("A Terreno é obrigatório");
           

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
