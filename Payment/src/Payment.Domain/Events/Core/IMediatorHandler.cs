using System.Threading.Tasks;

namespace Payment.Domain
{
    public interface IEventSourcingHandler
    {
        Task RaiseEvent<T>(T @event, string stream) where T : Event;
    }
}