using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Payment.Domain.Utilities
{
    public class ModelBase
    {
        protected IList<INotification> _events;

        public ModelBase()
        {
            _events = new List<INotification>();
        }

        public void RegisterEvent<T>(T domainEvent)
              where T : Event
        {
            _events.Add(domainEvent);
        }

    }
}
