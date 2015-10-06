using Orchard.Environment.Extensions;
using Orchard.Projections.Models;
using System.Collections.Generic;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.CalendarWidget")]
    public class EditCalendarWidgetViewModel {
        public IEnumerable<QueryPart> Queries { get; set; }
        public int QueryId { get; set; }
        public IEnumerable<string> Plugins { get; set; }
        public string Plugin { get; set; }
    }
}