using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace DQ.Scheduling.Drivers
{
    public class EventSubscribePartDriver : ContentPartDriver<EventSubscribePart> {
        protected override DriverResult Editor(EventSubscribePart part, dynamic shapeHelper) {
            return ContentShape("Parts_EventSubscribe_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/EventSubscribe",
                Model: part));
        }

        protected override DriverResult Editor(EventSubscribePart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}