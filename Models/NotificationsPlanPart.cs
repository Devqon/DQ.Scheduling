using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPlanPart : ContentPart<NotificationsPlanPartRecord> {
        public string Title {
            get {
                var titlePart = this.As<TitlePart>();
                return titlePart != null ? titlePart.Title : "";
            }
        }
        
        public NotificationIntervalType UpcomingNotificationInterval {
            get { return Retrieve(r => r.UpcomingNotificationInterval); }
            set { Store(r => r.UpcomingNotificationInterval, value); }
        }
        
        public int UpcomingNotificationIntervalCount {
            get { return Retrieve(r => r.UpcomingNotificationIntervalCount); }
            set { Store(r => r.UpcomingNotificationIntervalCount, value); }
        }

        public NotificationIntervalType FollowUpNotificationInterval {
            get { return Retrieve(r => r.FollowUpNotificationInterval); }
            set { Store(r => r.FollowUpNotificationInterval, value); }
        }

        public int FollowUpNotificationIntervalCount {
            get { return Retrieve(r => r.FollowUpNotificationIntervalCount); }
            set { Store(r => r.FollowUpNotificationIntervalCount, value); }
        }

    }
}