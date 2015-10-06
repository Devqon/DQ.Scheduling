using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPartRecord : ContentPartRecord {
        public virtual int EventId { get; set; }
        public virtual int UserId { get; set; } // TODO: this needs to be optional, should be able to take an email address / phone number on anonymous user
               
        public virtual SubscribeType SubscribeType { get; set; } // TODO: seems like they could choose both
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