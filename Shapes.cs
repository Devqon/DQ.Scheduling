using Orchard.DisplayManagement.Descriptors;

namespace DQ.Scheduling {
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