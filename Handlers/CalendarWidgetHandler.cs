using dsc.CalendarWidget.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace dsc.CalendarWidget.Handlers
{
    public class CalendarWidgetHandler:ContentHandler
    {
        public CalendarWidgetHandler(IRepository<CalendarWidgetPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}