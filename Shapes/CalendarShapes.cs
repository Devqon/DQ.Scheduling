using DQ.Scheduling.CalendarProviders;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DQ.Scheduling.SHapes {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarWidgetShapes : IShapeTableProvider { 
        private readonly IEnumerable<ICalendarProvider> _calendarProviders;
        public CalendarWidgetShapes(IEnumerable<ICalendarProvider> calendarProviders) {
            _calendarProviders = calendarProviders;
        }

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Parts_Calendar")
                .OnDisplaying(displaying => {
                    string plugin = displaying.Shape.Plugin;
                    var calendarProvider = _calendarProviders.FirstOrDefault(p => p.Name == plugin);

                    // Only if the provider is enabled
                    if (calendarProvider != null) {
                        displaying.ShapeMetadata.Alternates.Add("Parts_Calendar__" + plugin);
                    }
                });
        }
    }
}