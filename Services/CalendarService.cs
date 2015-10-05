using System.Collections.Generic;
using System.Linq;
using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Models;
using Orchard.Projections.Services;

namespace DQ.Scheduling.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IProjectionManager _projectionManager;
        private readonly IEnumerable<ICalendarProvider> _providers; 

        public CalendarService(IProjectionManager projectionManager, IEnumerable<ICalendarProvider> providers)
        {
            _projectionManager = projectionManager;
            _providers = providers;
        }

        public IEnumerable<SerializedEvent> GetEventDefinitions(CalendarWidgetPart part)
        {
            var contentItems = _projectionManager.GetContentItems(part.QueryId);

            var provider = _providers.SingleOrDefault(p => p.Name == part.Plugin);
            if (provider == null) {
                return new List<SerializedEvent>();
            }

            var models = provider.SerializeEvents(contentItems);

            return models;
        }

        public IList<string> GetCalendarPlugins() {

            return _providers.Select(p => p.Name).ToList();
        } 
    }
}