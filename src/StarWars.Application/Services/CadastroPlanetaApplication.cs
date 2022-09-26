using MediatR;
using Microsoft.Extensions.Logging;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.Application.Notifications;
using StarWars.Domain.Interfaces;
using StarWars.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Application.Services
{
    public class CadastroPlanetaApplication : BaseApplication, ICadastroPlanetaApplication
    {
        private readonly IMediatorHandler _mediator;
        private readonly IPlanetaRepository _planetaRepository;
        private readonly ILogger<CadastroPlanetaApplication> _logger;

        public CadastroPlanetaApplication(IMediatorHandler mediator, IUnitOfWork uow, IPlanetaRepository planetaRepository,
            ILogger<CadastroPlanetaApplication> logger,
        INotificationHandler<ApplicationNotification> notifications) : base(uow, mediator, notifications)
        {
            _mediator = mediator;
            _logger = logger;
            _planetaRepository = planetaRepository;
        }

        public async Task<PlanetaResultDto> AddPlaneta(PlanetaDto planetaDto)
        {
            if(await PlanetaJaCadastrado(planetaDto.Nome))
                return null;

            var planeta = new Planeta(planetaDto.Nome, planetaDto.Clima, planetaDto.Terreno);

            foreach (var filme in planetaDto.Filmes)
            {
                planeta.SetFilmes(filme.Nome, filme.Diretor, filme.DataLancamento);
            }

            if (!planeta.IsValid())
            {
                foreach (var error in planeta.ValidationResult.Errors)
                    await _mediator.PublishEvent(new ApplicationNotification(error.ErrorMessage));

                return null;
            }

            await _planetaRepository.AddAsync(planeta);
            var isCommit = await base.Commit();
            if (isCommit)
            {
                _logger.LogInformation($"Registro salvo com sucesso!");
                return await BuscaPlanetaPorId(planeta.Id);
            }
            return null;
        }

        public async Task<PlanetaResultDto> BuscaPlanetaPorNome(string nome)
        {
            var planeta = await _planetaRepository.ObterPlanetaPorNome(nome);
            if (planeta == null)
            {
                _logger.LogError($"Planeta com o nome: {nome} não encontrado");
                await _mediator.PublishEvent(new ApplicationNotification($"Planeta com o nome: {nome} não encontrado"));
                return null;
            }
            return planeta.MapToModel();
        }

        public async Task<PlanetaResultDto> BuscaPlanetaPorId(int id)
        {
            var planeta = await _planetaRepository.ObterPlanetaPorId(id);
            if (planeta == null)
            {
                _logger.LogError($"Planeta com o Id: {id} não encontrado");
                await _mediator.PublishEvent(new ApplicationNotification($"Planeta com o Id: {id} não encontrado"));
                return null;
            }
            return planeta.MapToModel();
        }

        public async Task<IEnumerable<PlanetaResultDto>> ListarTodosPlanetas()
        {
            var listaPlanetas = await _planetaRepository.ObterTodosPlanetas();
            return listaPlanetas.MapToModels();
        }

        public async Task<bool> DeletarPlaneta(int id)
        {
            var planeta = await _planetaRepository.ObterPlanetaPorId(id);

            if (planeta == null) {
                _logger.LogError($"Planeta com o Id: {id} não encontrado");
                await _mediator.PublishEvent(new ApplicationNotification($"Planeta com o Id: {id} não encontrado"));
                return false;
            }

            _planetaRepository.Remove(planeta);
            var isCommit = await base.Commit();
            return isCommit;
        }

        private async Task<bool> PlanetaJaCadastrado(string nome)
        {
            var planeta = _planetaRepository.ObterPlanetaPorNome(nome);
            if (planeta == null)
                return false;

            await _mediator.PublishEvent(new ApplicationNotification($"Planeta {nome} ja cadastrado na base de dados"));
            return true;
        }
    }
}
