using DQ.Scheduling.CalendarProviders;
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

        public IEnumerable<FormattedEvent> GetFormattedCalendarEvents(int queryId, string plugin) {
            var contentItems = _projectionManager.GetContentItems(queryId);
            var provider = GetProviderOrDefault(plugin);

            var models = provider.FormatCalendarEvents(contentItems);

            return models;
        }

        public ICalendarProvider GetProviderOrDefault(string pluginName) {

            if (string.IsNullOrEmpty(pluginName))
                return _calendarProviders.Single(cp => cp.Name == Constants.DefaultCalendarName);

            return _calendarProviders.FirstOrDefault(cp => cp.Name == pluginName) 
                ?? _calendarProviders.Single(cp => cp.Name == Constants.DefaultCalendarName);
        }

        public IEnumerable<ICalendarProvider> GetProviders() {
            return _calendarProviders;
        } 
    }
}