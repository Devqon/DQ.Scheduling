using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.CalendarWidget")]
    public class CalendarWidgetPartRecord : ContentPartRecord {
        public virtual int QueryId { get; set; }
        public virtual string Plugin { get; set; }
    }
}