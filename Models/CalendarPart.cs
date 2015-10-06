using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarPart : ContentPart<CalendarPartRecord> {
        public int QueryId {
            get { return Retrieve(x => x.QueryId); }
            set { Store(x => x.QueryId, value); }
        }

        public string Plugin {
            get { return Retrieve(x => x.Plugin); }
            set { Store(x => x.Plugin, value); }
        }
    }
}