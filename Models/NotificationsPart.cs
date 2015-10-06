using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPart : ContentPart<NotificationsPartRecord> {
        public bool AllowNotifications {
            get { return Retrieve(x => x.AllowNotifications); }
            set { Store(x => x.AllowNotifications, value); }
        }
    }
}