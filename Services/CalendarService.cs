using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Models;
using Orchard.Environment.Extensions;
using Orchard.Projections.Services;
using System.Collections.Generic;
using System.Linq;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarService : ICalendarService {
        private readonly IProjectionManager _projectionManager;
        private readonly IEnumerable<ICalendarProvider> _calendarProviders; 

        public CalendarService(IProjectionManager projectionManager, IEnumerable<ICalendarProvider> calendarProviders) {
            _projectionManager = projectionManager;
            _calendarProviders = calendarProviders;
        }

        public IEnumerable<FormattedEvent> GetFormattedCalendarEvents(CalendarPart part) {
            var contentItems = _projectionManager.GetContentItems(part.QueryId);

            var provider = _calendarProviders.SingleOrDefault(p => p.Name == part.Plugin);
            if (provider == null) {
                // fallback to default
                provider = _calendarProviders.Single(p => p.Name == "Default");
            }

            var models = provider.FormatCalendarEvents(contentItems);

            return models;
        }

        public IList<string> GetCalendarPlugins() {
            return _calendarProviders.Select(p => p.Name).ToList();
        } 
    }
}