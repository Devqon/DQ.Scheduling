using System.Linq;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
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
            IContentDefinitionManager contentDefinitionManager, 
            INotificationsService notificationsService) {

            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;

            Filters.Add(StorageFilter.For(repository));

            OnActivated<NotificationsPart>(LazyLoadHandlers);
            OnUpdated<NotificationsPart>((ctx, part) => notificationsService.UpdateScheduleTasks(part));
            OnUnpublished<NotificationsPart>((ctx, part) => notificationsService.DeleteExistingScheduleTasks(part.ContentItem));
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

            // If has part SchedulingPart, weld the NotificationsPart
            if (contentTypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof(SchedulingPart).Name)) {
                context.Builder.Weld<NotificationsPart>();
            }
        }
    }
}