using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPart : ContentPart<NotificationsPartRecord> {
        internal LazyField<NotificationsPlanPart> _contentItem = new LazyField<NotificationsPlanPart>();
        
        public bool AllowNotifications {
            get { return Retrieve(r => r.AllowNotifications); }
            set { Store(s => s.AllowNotifications, value); }
        }
        public NotificationsPlanPartRecord NotificationsPlanPartRecord {
            get { return Record.NotificationsPlanPartRecord; }
            set { Record.NotificationsPlanPartRecord = value; }
        }
        public NotificationsPlanPart NotificationsPlanPart {
            get {
                return _contentItem.Value;
            }
        }
    }
}