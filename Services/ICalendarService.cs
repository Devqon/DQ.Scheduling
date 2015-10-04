using System.Collections.Generic;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using Orchard.Projections.Models;

namespace DQ.Scheduling.Services
{
    public interface ICalendarService : IDependency
    {
        IEnumerable<CalendarEventViewModel> GetCalendarEvents(CalendarWidgetPart part);
        List<QueryPart> GetCalendarQueries();
        IList<string> GetCalendarPlugins();
    }
}