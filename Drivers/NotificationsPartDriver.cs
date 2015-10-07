using System.Linq;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Services;
using System.Xml.Linq;
using Orchard.Core.Title.Models;
using System.Collections.Generic;
using DQ.Scheduling.ViewModels;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartDriver : ContentPartDriver<NotificationsPart> {
        private readonly IClock _clock;
        private readonly INotificationsService _notificationsService;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        public NotificationsPartDriver(IClock clock
            , INotificationsService notificationsService
            , IWorkContextAccessor workContextAccessor
            , IContentManager contentManager) {

            _clock = clock;
            _notificationsService = notificationsService;
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }

        private const string NotificationsPlanName = "NotificationsPlan";

        protected override string Prefix {
            get { return "SchedulingNotifications"; }
        }

        // TODO: This whole display area needs Reworked
        protected override DriverResult Display(NotificationsPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_NotificationsForm", 
                () => {
                    var contentItem = part.ContentItem;
                    var schedulingPart = contentItem.As<SchedulingPart>();

                    // Cannot subscribe to events in the past
                    // TODO: Should make a shared method to determine whether an event is in the future, such as ScheduleService.GetNextOccurrence
                    if (schedulingPart == null || (schedulingPart.StartDateTime < _clock.UtcNow && !schedulingPart.IsRecurring))
                        return null;

                    // TODO: don't want to use current user as the basis of whether or not someone has subscribed as that would mean authenticated users only
                    var user = _workContextAccessor.GetContext().CurrentUser;

                    // Already subscribed
                    var existingSubscription = _notificationsService.GetSubscriptions(schedulingPart.Id, user.Id).FirstOrDefault();

                    return shapeHelper.Parts_NotificationsForm(
                        Subscribed: existingSubscription != null,
                        Event: part.ContentItem,
                        Subscription: existingSubscription ?? new NotificationsSubscriptionPartRecord { EventId = part.Id, UserId = user.Id }
                        );
                });
        }

        //GET
        protected override DriverResult Editor(NotificationsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Notifications_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/Notifications",
                Model: new NotificationsEditViewModel {
                    ContentId = part.Id,
                    AllowNotifications = part.AllowNotifications,
                    NotificationsPlanId = part.NotificationsPlanPartRecord == null ? -1 : part.NotificationsPlanPartRecord.Id,
                    NotificationPlans = GetNotificationsPlans()
                },
                Prefix: Prefix));
        }
        
        private Dictionary<int, string> GetNotificationsPlans() {
            return _contentManager.Query<NotificationsPlanPart, NotificationsPlanPartRecord>(VersionOptions.Published)
                .WithQueryHints(new QueryHints().ExpandParts<TitlePart>())
                .List()
                .ToDictionary(sp => sp.Id, sp => _contentManager.GetItemMetadata(sp.ContentItem).DisplayText);
        }

        //POST
        protected override DriverResult Editor(NotificationsPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new NotificationsEditViewModel();

            if (updater.TryUpdateModel(model, Prefix, null, null)) {
                if (model.NotificationsPlanId.HasValue) {
                    var notificationsPlan = _contentManager.Get(model.NotificationsPlanId.Value).As<NotificationsPlanPart>();
                    part.NotificationsPlanPartRecord = notificationsPlan == null ? null : notificationsPlan.Record;
                }
                else {
                    part.NotificationsPlanPartRecord = null;
                }
            }
            return Editor(part, shapeHelper);
        }


        protected override void Importing(NotificationsPart part, ImportContentContext context) {
            var el = context.Data.Element(typeof(NotificationsPart).Name);
            if (el == null) return;
            el.With(part)
                .FromAttr(p => p.AllowNotifications);
            var notificationsPlanEl = el.Element(NotificationsPlanName);
            if (notificationsPlanEl != null) {
                var notificationsPlanIdentity = notificationsPlanEl.Attr("Id");
                if (!string.IsNullOrEmpty(notificationsPlanIdentity)) {
                    var contentItem = context.GetItemFromSession(notificationsPlanIdentity);
                    if (contentItem != null) {
                        var notificationsPlanPart = contentItem.As<NotificationsPlanPart>();
                        if (notificationsPlanPart != null) {
                            part.NotificationsPlanPartRecord = notificationsPlanPart.Record;
                        }
                    }
                }
            }                
        }

        protected override void Exporting(NotificationsPart part, ExportContentContext context) {
            var el = context.Data.Element(typeof(NotificationsPart).Name);
            el.With(part)
                .ToAttr(p => p.AllowNotifications);
            el.AddEl(new XElement(NotificationsPlanName)
                .Attr("Id", _contentManager.GetItemMetadata(part).Identity));

        }
    }
}