using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribePartDriver : ContentPartDriver<EventSubscribePart> {
        private readonly IClock _clock;
        public EventSubscribePartDriver(IClock clock) {
            _clock = clock;
        }

        protected override DriverResult Display(EventSubscribePart part, string displayType, dynamic shapeHelper) {
            var contentItem = part.ContentItem;
            var eventDefinitionPart = contentItem.As<EventDefinitionPart>();

            // Cannot subscribe to events in the past
            // TODO: Should make a shared method to determine whether an event is in the future, such as ScheduleService.GetNextOccurrence
            if (eventDefinitionPart == null || (eventDefinitionPart.StartDateTime < _clock.UtcNow && !eventDefinitionPart.IsRecurring))
                return null;

            return ContentShape("Parts_EventSubscribeForm", () => shapeHelper.Parts_EventSubscribeForm(
                Event: part.ContentItem,
                Subscription: new EventSubscriptionRecord{ EventId = part.Id }));
        }

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