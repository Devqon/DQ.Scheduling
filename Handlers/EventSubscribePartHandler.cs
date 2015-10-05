using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers
{
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribePartHandler : ContentHandler
    {
        public EventSubscribePartHandler(IRepository<EventSubscribePartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<EventSubscribePart>((ctx, part) => UpdateSubscribers(part));
        }

        protected override void Activated(ActivatedContentContext context) {
            var eventDefinitionPart = context.ContentItem.As<EventDefinitionPart>();
            if (eventDefinitionPart == null)
                return;

            // TODO:
            // context.ContentItem.Weld(new EventSubscribePart());
        }

        private void UpdateSubscribers(EventSubscribePart part) {
            
        }
    }
}