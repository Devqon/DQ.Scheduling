using Orchard.ContentManagement.Records;

namespace DQ.Scheduling.Models
{
    public class EventSubscribePartRecord : ContentPartRecord
    {
        public SubscribeType SubscribeType { get; set; }
        public int TimeDifference { get; set; }
        public SubscribeDifference SubscribeDifference { get; set; }
    }

    public enum SubscribeDifference {
        None,
        Days,
        Hours,
        Minutes
    }
}