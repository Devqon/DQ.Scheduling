using System.Collections.Generic;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;

namespace DQ.Scheduling.Services
{
    public interface ICalendarService : IDependency
    {
        IEnumerable<EventDefinitionViewModel> GetEventDefinitions(CalendarWidgetPart part);
        IList<string> GetCalendarPlugins();
    }
}