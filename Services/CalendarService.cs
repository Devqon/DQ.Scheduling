using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Models;
using Orchard.Environment.Extensions;
using Orchard.Projections.Services;
using System.Collections.Generic;
using System.Linq;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.CalendarWidget")]
    public class CalendarService : ICalendarService {
        private readonly IProjectionManager _projectionManager;
        private readonly IEnumerable<ICalendarProvider> _providers; 

        public CalendarService(IProjectionManager projectionManager, IEnumerable<ICalendarProvider> providers) {
            _projectionManager = projectionManager;
            _providers = providers;
        }

        public IEnumerable<SerializedEvent> GetEventDefinitions(CalendarWidgetPart part) {
            var contentItems = _projectionManager.GetContentItems(part.QueryId);

            var provider = _providers.SingleOrDefault(p => p.Name == part.Plugin);
            if (provider == null) {
                // fallback to default
                provider = _providers.Single(p => p.Name == "Default");
            }

            var models = provider.SerializeEvents(contentItems);

            return models;
        }

        public IList<string> GetCalendarPlugins() {
            return _providers.Select(p => p.Name).ToList();
        } 
    }
}