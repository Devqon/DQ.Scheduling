using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System.Linq;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribePartHandler : ContentHandler  {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public EventSubscribePartHandler(IRepository<EventSubscribePartRecord> repository, IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<EventSubscribePart>((ctx, part) => UpdateSubscribers(part));
        }

        protected override void Activating(ActivatingContentContext context) {
            base.Activating(context);

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null)
                return;

            // If has part EventDefinition, weld the EventSubscribe part
            if (contentTypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof (EventDefinitionPart).Name)) {
                context.Builder.Weld<EventSubscribePart>();
            }
        }

        private void UpdateSubscribers(EventSubscribePart part) {
            
        }
    }
}