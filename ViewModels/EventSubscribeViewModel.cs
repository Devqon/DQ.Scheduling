using DQ.Scheduling.Models;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribeViewModel {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public SubscribeType SubscribeType { get; set; }
        public int TimeDifference { get; set; }
        public SubscribeDifference SubscribeDifference { get; set; }
    }
}