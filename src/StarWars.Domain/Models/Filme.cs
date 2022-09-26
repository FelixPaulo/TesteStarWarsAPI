using StarWars.Domain.Core;
using System;

namespace StarWars.Domain.Models
{
    public class Filme : Entity<Filme>
    {
        public Filme() { }

        public Filme(string nome, string diretor, DateTime dataLancamento)
        {
            Nome = nome;
            Diretor = diretor;
            DataLancamento = dataLancamento != null ? dataLancamento : DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Diretor { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public int PlanetaId { get; private set; }
        public Planeta Planeta { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
