using StarWars.Application.Events;
using System.Threading.Tasks;

namespace StarWars.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}
