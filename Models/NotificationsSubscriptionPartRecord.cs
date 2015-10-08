using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPartRecord : ContentPartRecord {

        // Optional, can also do anonymous
        public virtual int? UserId { get; set; }

        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
               
        public virtual SubscribeType SubscribeType { get; set; }
    }
}