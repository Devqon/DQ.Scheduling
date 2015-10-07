using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPlanPartRecord : ContentPartRecord {
        public virtual NotificationIntervalType UpcomingNotificationInterval { get; set; }
        public virtual int UpcomingNotificationIntervalCount { get; set; }
        public virtual NotificationIntervalType FollowUpNotificationInterval { get; set; }
        public virtual int FollowUpNotificationCount { get; set; }
    }
}