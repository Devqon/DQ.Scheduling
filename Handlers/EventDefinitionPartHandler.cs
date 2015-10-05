using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace DQ.Scheduling.Handlers
{
    public class EventDefinitionPartHandler : ContentHandler
    {
        public EventDefinitionPartHandler(IRepository<EventDefinitionPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}