using MediatR;
using StarWars.Application.Interfaces;
using StarWars.Application.Notifications;
using StarWars.Domain.Interfaces;
using System.Threading.Tasks;

namespace StarWars.Application.Services
{
    public class BaseApplication
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly ApplicationNotificationHandler _notifications;


        public BaseApplication(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<ApplicationNotification> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (ApplicationNotificationHandler)notifications;
        }

        protected async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;

            if (_uow.Commit())
                return true;

            await _mediator.PublishEvent(new ApplicationNotification("Ocorreu um erro ao salvar o Planeta :("));
            return false;
        }
    }
}
