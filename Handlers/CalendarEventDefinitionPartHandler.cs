using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace DQ.Scheduling.Handlers
{
    public class CalendarEventDefinitionPartHandler : ContentHandler
    {
        public CalendarEventDefinitionPartHandler(IRepository<CalendarEventDefinitionRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}