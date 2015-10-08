using System.Linq;
using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartHandler : ContentHandler  {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public NotificationsPartHandler(
            IRepository<NotificationsPartRecord> repository, 
            IContentManager contentManager, 
            IContentDefinitionManager contentDefinitionManager) {

            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;

            Filters.Add(StorageFilter.For(repository));

            OnActivated<NotificationsPart>(LazyLoadHandlers);
        }
        
        private void LazyLoadHandlers(ActivatedContentContext context, NotificationsPart part) {
            part._contentItem.Loader(() => part.NotificationsPlanPartRecord == null ? null : _contentManager.Get<NotificationsPlanPart>(part.NotificationsPlanPartRecord.Id));
        }

        protected override void Activating(ActivatingContentContext context)
        {
            base.Activating(context);

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null)
                return;

            // If has part EventDefinition, weld the EventSubscribe part
            if (contentTypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof(SchedulingPart).Name)) {
                context.Builder.Weld<NotificationsPart>();
            }
        }
    }
}