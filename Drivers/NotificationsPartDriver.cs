using System.Linq;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartDriver : ContentPartDriver<NotificationsPart> {
        private readonly IClock _clock;
        private readonly INotificationsService _subscriptionService;
        private readonly IWorkContextAccessor _workContextAccessor;

        public NotificationsPartDriver(IClock clock, INotificationsService subscriptionService, IWorkContextAccessor workContextAccessor) {
            _clock = clock;
            _subscriptionService = subscriptionService;
            _workContextAccessor = workContextAccessor;
        }

        protected override DriverResult Display(NotificationsPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_NotificationsForm", 
                () => {
                    var contentItem = part.ContentItem;
                    var eventDefinitionPart = contentItem.As<SchedulingPart>();

                    // Cannot subscribe to events in the past
                    // TODO: Should make a shared method to determine whether an event is in the future, such as ScheduleService.GetNextOccurrence
                    if (eventDefinitionPart == null || (eventDefinitionPart.StartDateTime < _clock.UtcNow && !eventDefinitionPart.IsRecurring))
                        return null;

                    var user = _workContextAccessor.GetContext().CurrentUser;

                    // Already subscribed
                    var existingSubscription = _subscriptionService.GetSubscriptions(eventDefinitionPart.Id, user.Id).FirstOrDefault();

                    return shapeHelper.Parts_NotificationsForm(
                        Subscribed: existingSubscription != null,
                        Event: part.ContentItem,
                        Subscription: existingSubscription ?? new NotificationsSubscriptionPartRecord { EventId = part.Id, UserId = user.Id }
                        );
                });
        }

        protected override DriverResult Editor(NotificationsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Notifications_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/Notifications",
                Model: part));
        }

        protected override DriverResult Editor(NotificationsPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}