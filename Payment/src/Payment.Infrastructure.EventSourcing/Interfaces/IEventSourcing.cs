using Payment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.EventSourcing
{
    public interface IEventSourcing
    {
        Task AppendEventOnStreamAsync<T>(T @event, string stream) where T : Event; 
    }
}
