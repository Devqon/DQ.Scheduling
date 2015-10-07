using System.Collections.Generic;
using Orchard;
using Orchard.ContentManagement;

namespace DQ.Scheduling.CalendarProviders {
    public interface ICalendarProvider : IDependency {
        /// <summary>
        /// A unique name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Serialize the events to the desired format
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        IEnumerable<FormattedEvent> FormatCalendarEvents(IEnumerable<IContent> events);
    }
}