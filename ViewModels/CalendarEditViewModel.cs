using DQ.Scheduling.CalendarProviders;
using Orchard.Environment.Extensions;
using Orchard.Projections.Models;
using System.Collections.Generic;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarEditViewModel {
        public IEnumerable<QueryPart> Queries { get; set; }
        public int QueryId { get; set; }
        public IEnumerable<ICalendarProvider> Plugins { get; set; }
        public string Plugin { get; set; }
        public bool UseAsync { get; set; }
    }
}