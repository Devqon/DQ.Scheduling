using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribePart : ContentPart<EventSubscribePartRecord> {
        public bool AllowSubscriptions {
            get { return Retrieve(x => x.AllowSubscriptions); }
            set { Store(x => x.AllowSubscriptions, value); }
        }
    }
}