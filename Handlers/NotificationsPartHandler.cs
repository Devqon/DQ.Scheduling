using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System.Linq;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartHandler : ContentHandler  {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public NotificationsPartHandler(IRepository<NotificationsPartRecord> repository, IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<NotificationsPart>((ctx, part) => UpdateSubscribers(part));
        }

        protected override void Activating(ActivatingContentContext context) {
            base.Activating(context);

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null)
                return;

            // If has part EventDefinition, weld the EventSubscribe part
            if (contentTypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof (SchedulingPart).Name)) {
                context.Builder.Weld<NotificationsPart>();
            }
        }

        private void UpdateSubscribers(NotificationsPart part) {
            
        }
    }
}