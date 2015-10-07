using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartHandler : ContentHandler  {
        private readonly IContentManager _contentManager;

        public NotificationsPartHandler(IRepository<NotificationsPartRecord> repository
            , IContentManager contentManager) {
            
            Filters.Add(StorageFilter.For(repository));
            _contentManager = contentManager;
            
            OnActivated<NotificationsPart>(LazyLoadHandlers);
        }
        
        private void LazyLoadHandlers(ActivatedContentContext context, NotificationsPart part) {
            part._contentItem.Loader(() => part.NotificationsPlanPartRecord == null ? null : _contentManager.Get<NotificationsPlanPart>(part.NotificationsPlanPartRecord.Id));
        }
    }
}