using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPlanPartHandler : ContentHandler  {
        public NotificationsPlanPartHandler(IRepository<NotificationsPlanPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}