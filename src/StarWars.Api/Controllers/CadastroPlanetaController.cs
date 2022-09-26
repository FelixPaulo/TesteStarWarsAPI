using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.Application.Notifications;
using System.Net;
using System.Threading.Tasks;

namespace StarWars.Api.Controllers
{
    public class CadastroPlanetaController : BaseController
    {
        private readonly ICadastroPlanetaApplication _cadastroPlanetaApplication;
        private readonly ILogger<CadastroPlanetaController> _logger;

        public CadastroPlanetaController(ILogger<CadastroPlanetaController> logger, INotificationHandler<ApplicationNotification> notifications, ICadastroPlanetaApplication cadastroPlanetaApplication) : base(notifications)
        {
            _logger = logger;
            _cadastroPlanetaApplication = cadastroPlanetaApplication;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(PlanetaDto planetaDto)
        {
            if (!ModelState.IsValid)
                return base.ResponseError(ModelState.Values);

            _logger.LogInformation("Iniciando o cadastro do planeta");
            var result = await _cadastroPlanetaApplication.AddPlaneta(planetaDto);


            return base.ResponseResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorId(id);
            return base.ResponseResult(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _cadastroPlanetaApplication.ListarTodosPlanetas();
            return base.ResponseResult(result);
        }

        [HttpGet("nome")]
        public async Task<IActionResult> Get(string nome)
        {
            var result = await _cadastroPlanetaApplication.BuscaPlanetaPorNome(nome);
            return base.ResponseResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cadastroPlanetaApplication.DeletarPlaneta(id);
            return base.ResponseResult(result);
        }
    }
}
