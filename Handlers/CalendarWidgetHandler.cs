using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.CalendarWidget")]
    public class CalendarWidgetHandler:ContentHandler {
        public CalendarWidgetHandler(IRepository<CalendarWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}