using System.Collections.Generic;
using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;

namespace DQ.Scheduling.Helpers
{
    public static class CalendarHelpers {
        public static IEnumerable<FormattedEvent> GetFormattedCalendarEvents(this ICalendarService calendarService, CalendarPart part) {
            return calendarService.GetFormattedCalendarEvents(part.QueryId, part.Plugin);
        }
    }
}