using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Application.Handlers;
using StarWars.Application.Interfaces;
using StarWars.Application.Notifications;
using StarWars.Application.Services;
using StarWars.Domain.Interfaces;
using StarWars.Infra.Data.Context;
using StarWars.Infra.Data.Repository;
using StarWars.Infra.Data.UoW;

namespace StarWars.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application
            services.AddScoped<ICadastroPlanetaApplication, CadastroPlanetaApplication>();
            services.AddScoped<INotificationHandler<ApplicationNotification>, ApplicationNotificationHandler>();

            //Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlanetaRepository, PlanetaRepository>();
          

            // Context
            services.AddScoped<DataBaseContext>();
        }
    }
}
