using System.Collections.Generic;
using System.Linq;
using DQ.Scheduling.CalendarProviders;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling {
    [OrchardFeature("DQ.CalendarWidget")]
    public class Shapes : IShapeTableProvider {

        private readonly IEnumerable<ICalendarProvider> _providers;
        public Shapes(IEnumerable<ICalendarProvider> providers) {
            _providers = providers;
        }

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Parts_CalendarWidget")
                .OnDisplaying(displaying => {
                    string plugin = displaying.Shape.Plugin;
                    var provider = _providers.FirstOrDefault(p => p.Name == plugin);

                    // Only if the provider is enabled
                    if (provider != null) {
                        displaying.ShapeMetadata.Alternates.Add("Parts_CalendarWidget__" + plugin);
                    }
                });
        }
    }
}