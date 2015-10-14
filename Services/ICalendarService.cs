using DQ.Scheduling.CalendarProviders;
using Orchard;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    public interface ICalendarService : IDependency {
        IEnumerable<FormattedEvent> GetFormattedCalendarEvents(int queryId, string plugin);

        /// <summary>
        /// Get provider by plugin name
        /// If doesn't exist, return default plugin
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        ICalendarProvider GetProviderOrDefault(string pluginName);

        IEnumerable<ICalendarProvider> GetProviders();
    }
}