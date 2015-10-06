using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartRecord : ContentPartRecord {
        public virtual bool AllowNotifications { get; set; }
    }
}