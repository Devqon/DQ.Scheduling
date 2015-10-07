using Orchard.Environment.Extensions;
using System.Collections.Generic;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsEditViewModel {
        public int ContentId { get; set; }
        public bool AllowNotifications { get; set; }
        public int? NotificationsPlanId { get; set; }
        public Dictionary<int, string> NotificationPlans { get; set; }
    }
}