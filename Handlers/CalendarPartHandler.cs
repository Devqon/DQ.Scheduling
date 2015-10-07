using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarPartHandler : ContentHandler {
        public CalendarPartHandler(IRepository<CalendarPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}