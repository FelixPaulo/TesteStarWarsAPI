using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.Application.Notifications;
using StarWars.Application.Services;
using StarWars.Domain.Interfaces;
using StarWars.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.Application.Testes
{
    public class CadastroPlanetaApplicationTeste
    {
        private readonly CadastroPlanetaApplication _cadastroPlanetaApplication;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPlanetaRepository> _planetaRepository;
        private readonly Mock<ILogger<CadastroPlanetaApplication>> _logger;
        private readonly Mock<ApplicationNotificationHandler> _notifications;

        public CadastroPlanetaApplicationTeste()
        {
            _mediator = new Mock<IMediatorHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _planetaRepository = new Mock<IPlanetaRepository>();
            _logger = new Mock<ILogger<CadastroPlanetaApplication>>();
            _notifications = new Mock<ApplicationNotificationHandler>();

            _cadastroPlanetaApplication = new CadastroPlanetaApplication(_mediator.Object, _unitOfWork.Object, _planetaRepository.Object, _logger.Object, _notifications.Object);
        }

        [Fact]
        public async Task DeveRetornarUmPlanetaSeCadastradoComSucesso()
        {
            //Arrange
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var filmes = new List<FilmesDto> { new FilmesDto { Nome = "Filme teste", DataLancamento = DateTime.UtcNow, Diretor = "Teste" } };
            var planetaDto = new PlanetaDto { Nome = "Planeta teste", Clima = "Clima teste", Terreno = "Terreno teste", Filmes = filmes };
            _planetaRepository.Setup(x => x.ObterPlanetaPorId(It.IsAny<int>())).ReturnsAsync(CriandoPlanetaFake());

            //Act
            var result = await _cadastroPlanetaApplication.AddPlaneta(planetaDto);

            //Assert
            result.Should().NotBeNull();
            result.Filmes.Count().Should().Be(2);
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeveRetornarNuloComErroSeDominioNaoForValido()
        {
            //Arrange
            var filmes = new List<FilmesDto> { new FilmesDto { Nome = "Filme teste", DataLancamento = DateTime.UtcNow, Diretor = "Teste" } };
            var planetaDto = new PlanetaDto { Clima = "Clima teste", Terreno = "Terreno teste", Filmes = filmes };

            //Act
            var result = await _cadastroPlanetaApplication.AddPlaneta(planetaDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "O nome é obrigatório")));
        }

        [Fact]
        public async Task DeveRetornarNuloSeOcorrerErroAoSalvarRegistro()
        {
            //Arrange
            _unitOfWork.Setup(x => x.Commit()).Returns(false);
            var filmes = new List<FilmesDto> { new FilmesDto { Nome = "Filme teste", DataLancamento = DateTime.UtcNow, Diretor = "Teste" } };
            var planetaDto = new PlanetaDto { Nome = "Planeta teste", Clima = "Clima teste", Terreno = "Terreno teste", Filmes = filmes };

            //Act
            var result = await _cadastroPlanetaApplication.AddPlaneta(planetaDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Ocorreu um erro ao salvar o Planeta :(")));
        }

        [Fact]
        public async Task DeveRetornarTrueSeItemDeletadoComSucesso()
        {
            //Arrange
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            _planetaRepository.Setup(x => x.ObterPlanetaPorId(It.IsAny<int>())).ReturnsAsync(CriandoPlanetaFake());

            //Act
            var result = await _cadastroPlanetaApplication.DeletarPlaneta(1);

            //Assert
            result.Should().BeTrue();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeveRetornarFalseSeNaoEncontrarPlanetaPorIdAoDeletar()
        {
            //Arrange
            Planeta planeta = null;
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            _planetaRepository.Setup(x => x.ObterPlanetaPorId(It.IsAny<int>())).ReturnsAsync(planeta);

            //Act
            var result = await _cadastroPlanetaApplication.DeletarPlaneta(1);

            //Assert
            result.Should().BeFalse();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Planeta com o Id: 1 não encontrado")));
        }

        [Fact]
        public async Task DeveRetornarUmaListaDePlanetasComFilmes()
        {
            //Arrange
            var lstPlaneta = new List<Planeta> { CriandoPlanetaFake() };
            _planetaRepository.Setup(x => x.ObterTodosPlanetas()).ReturnsAsync(lstPlaneta);

            //Act
            var resultList = await _cadastroPlanetaApplication.ListarTodosPlanetas();

            //Assert
            resultList.Should().NotBeEmpty();
            resultList.Count().Should().Be(1);
            resultList.First().Filmes.Count().Should().Be(2);
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeveRetornarUmPlanetaBuscadoPorNome()
        {
            //Arrange
            _planetaRepository.Setup(x => x.ObterPlanetaPorNome(It.IsAny<string>())).ReturnsAsync(CriandoPlanetaFake());

            //Act
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorNome("Filme teste");

            //Assert
            result.Should().NotBeNull();
            result.Filmes.Count().Should().Be(2);
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeveRetornarNuloCasoNaoEncontrePlanetaPorNome()
        {
            //Arrange

            //Act
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorNome("Filme teste");

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Planeta com o nome: Filme teste não encontrado")));
        }


        [Fact]
        public async Task DeveRetornarUmPlanetaBuscadoPorId()
        {
            //Arrange
            _planetaRepository.Setup(x => x.ObterPlanetaPorId(It.IsAny<int>())).ReturnsAsync(CriandoPlanetaFake());

            //Act
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorId(1);

            //Assert
            result.Should().NotBeNull();
            result.Filmes.Count().Should().Be(2);
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeveRetornarNuloCasoNaoEncontrePlanetaPorId()
        {
            //Arrange

            //Act
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorId(10);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Planeta com o Id: 10 não encontrado")));
        }

        private Planeta CriandoPlanetaFake()
        {
            var planeta = new Planeta("Filme teste", "Clima teste", "Terreno teste");
            planeta.SetFilmes("Filme teste", "Teste", DateTime.UtcNow);
            planeta.SetFilmes("Filme teste 007", "Teste 007", DateTime.UtcNow);
            return planeta;
        }
    }
}
