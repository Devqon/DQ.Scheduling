using System;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingPart : ContentPart<SchedulingPartRecord> {

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

        public string DisplayUrlOverride {
            get { return Retrieve(x => x.DisplayUrlOverride); }
            set { Store(x => x.DisplayUrlOverride, value); }
        }

        public bool ShowTime {
            get { return !IsAllDay; }
        }
    }
}
