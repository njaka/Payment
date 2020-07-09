using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Api.Configuration.Model
{
    public class EventSourcingConfigurationModel
    {
        /// <summary>
        /// Gets or sets the Event Sourcing ConnectionString.
        /// </summary>
        /// <value>
        /// The default version.
        /// </value>
        public string ConnectionString { get; set; }

    }
}
