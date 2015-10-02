using dsc.CalendarWidget.Models;
using dsc.CalendarWidget.ViewModels;
using Orchard;
using Orchard.Projections.Models;
using System.Collections.Generic;

namespace dsc.CalendarWidget.Services
{
    public interface ICalendarService : IDependency
    {
        IEnumerable<CalendarEventViewModel> GetCalendarEvents(CalendarWidgetPart part);
        List<QueryPart> GetCalendarQueries();
    }
}