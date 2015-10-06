using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarHandler:ContentHandler {
        public CalendarHandler(IRepository<CalendarPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}