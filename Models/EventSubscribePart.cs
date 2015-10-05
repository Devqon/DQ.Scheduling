using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class EventSubscribePart : ContentPart<EventSubscribePartRecord> {

        public SubscribeType SubscribeType {
            get { return Retrieve(x => x.SubscribeType); }
            set { Store(x => x.SubscribeType, value); }
        }

        public int TimeDifference
        {
            get { return Retrieve(x => x.TimeDifference); }
            set { Store(x => x.TimeDifference, value); }
        }

        public SubscribeDifference SubscribeDifference
        {
            get { return Retrieve(x => x.SubscribeDifference); }
            set { Store(x => x.SubscribeDifference, value); }
        }
    }
}