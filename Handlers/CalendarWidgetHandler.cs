using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace DQ.Scheduling.Handlers
{
    public class CalendarWidgetHandler:ContentHandler
    {
        public CalendarWidgetHandler(IRepository<CalendarWidgetPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}