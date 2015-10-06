using System;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.Scheduling")]
    public class EventDefinitionPart : ContentPart<EventDefinitionPartRecord> {
        // TODO: Do we really need to record TimeZone if we are recording datetime in UTC?
        public string TimeZone {
            get { return Retrieve(x => x.TimeZone); }
            set { Store(x => x.TimeZone, value); }
        }

        public DateTime? StartDateTime {
            get { return Retrieve(x => x.StartDateTime); }
            set { Store(x => x.StartDateTime, value); }
        }

        public DateTime? EndDateTime {
            get { return Retrieve(x => x.EndDateTime); }
            set { Store(x => x.EndDateTime, value); }
        }

        public bool IsAllDay {
            get { return Retrieve(x => x.IsAllDay); }
            set { Store(x => x.IsAllDay, value); }
        }

        public bool IsRecurring {
            get { return Retrieve(x => x.IsRecurring); }
            set { Store(x => x.IsRecurring, value); }
        }

        public bool ShowTime {
            get { return !IsAllDay; }
        }
    }
}
