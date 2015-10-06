using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarPartRecord : ContentPartRecord {
        public virtual int QueryId { get; set; }
        public virtual string Plugin { get; set; }
    }
}