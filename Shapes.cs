using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling {
    [OrchardFeature("DQ.CalendarWidget")]
    public class Shapes : IShapeTableProvider {

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Parts_CalendarWidget")
                .OnDisplaying(displaying => {
                    string plugin = displaying.Shape.Plugin;
                    displaying.ShapeMetadata.Alternates.Add("Parts_CalendarWidget__" + plugin);
                });
        }
    }
}