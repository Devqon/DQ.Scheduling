using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPartRecord : ContentPartRecord {
        public virtual int EventId { get; set; }

        // Optional, can also do anonymous
        public virtual int? UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
               
        public virtual SubscribeType SubscribeType { get; set; }

        // TODO: remove following properties? let admin define when a notification is to be send
        public virtual int TimeDifference { get; set; }
        public virtual SubscribeDifference SubscribeDifference { get; set; }
    }

    public enum SubscribeDifference {
        None,
        Days,
        Hours,
        Minutes
    }
}