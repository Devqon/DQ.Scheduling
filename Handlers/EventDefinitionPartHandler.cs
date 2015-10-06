using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers  {
    [OrchardFeature("DQ.Scheduling")]
    public class EventDefinitionPartHandler : ContentHandler {
        public EventDefinitionPartHandler(IRepository<EventDefinitionPartRecord> repository, IEventService eventService) {
            Filters.Add(StorageFilter.For(repository));

            // TODO: only when some feature is enabled?
            OnUpdated<EventDefinitionPart>((ctx, part) => eventService.ScheduleEvent(part));
        }
    }
}