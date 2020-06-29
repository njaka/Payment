using System.Threading.Tasks;

namespace Payment.Domain
{
    public interface IMediatorHandler
    {
        Task<object> SendCommand<T>(T command);
        Task RaiseEvent<T>(T @event, string stream) where T : Event;
    }
}