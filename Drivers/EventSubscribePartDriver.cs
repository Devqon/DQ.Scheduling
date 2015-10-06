using System.Linq;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Services;

namespace DQ.Scheduling.Drivers
{
    public class EventSubscribePartDriver : ContentPartDriver<EventSubscribePart> {
        private readonly IClock _clock;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IWorkContextAccessor _workContextAccessor;

        public EventSubscribePartDriver(IClock clock, ISubscriptionService subscriptionService, IWorkContextAccessor workContextAccessor) {
            _clock = clock;
            _subscriptionService = subscriptionService;
            _workContextAccessor = workContextAccessor;
        }

        protected override DriverResult Display(EventSubscribePart part, string displayType, dynamic shapeHelper) {

            var contentItem = part.ContentItem;
            var eventDefinitionPart = contentItem.As<EventDefinitionPart>();

            // Cannot subscribe to events in the past
            if (eventDefinitionPart == null || (eventDefinitionPart.StartDateTime < _clock.UtcNow && !eventDefinitionPart.IsRecurring))
                return null;

            var user = _workContextAccessor.GetContext().CurrentUser;

            // Already subscribed
            if (_subscriptionService.GetSubscriptions(eventDefinitionPart.Id, user.Id).Any())
                return null;

            return ContentShape("Parts_EventSubscribeForm", () => shapeHelper.Parts_EventSubscribeForm(
                Event: part.ContentItem,
                Subscription: new EventSubscriptionRecord{ EventId = part.Id, UserId = user.Id }));
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