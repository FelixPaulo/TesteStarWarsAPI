using MediatR;
using StarWars.Application.Events;
using StarWars.Application.Interfaces;
using System.Threading.Tasks;

namespace StarWars.Application.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
