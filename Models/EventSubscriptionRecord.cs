using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscriptionRecord {
        public virtual int Id { get; set; }
        public virtual int EventId { get; set; }
        public virtual int UserId { get; set; }
               
        public virtual SubscribeType SubscribeType { get; set; }
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