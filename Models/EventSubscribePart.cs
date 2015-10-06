using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class EventSubscribePart : ContentPart<EventSubscribePartRecord> {

        public bool AllowSubscriptions {
            get { return Retrieve(x => x.AllowSubscriptions); }
            set { Store(x => x.AllowSubscriptions, value); }
        }
    }
}