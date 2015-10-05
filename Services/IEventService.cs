using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQ.Scheduling.Models;
using Orchard;
using Orchard.Projections.Models;

namespace DQ.Scheduling.Services
{
    public interface IEventService : IDependency
    {
        void NotifyEventSubscribers(EventDefinitionPart eventDefinitionPart);
        List<QueryPart> GetEventDefinitionQueries();
        void SubscribeToEvent(EventDefinitionPart eventDefinitionPart, SubscribeType subscribeType);
    }
}
