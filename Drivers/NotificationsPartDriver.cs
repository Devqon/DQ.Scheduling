using System.Linq;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using System.Xml.Linq;
using Orchard.Core.Title.Models;
using System.Collections.Generic;
using DQ.Scheduling.ViewModels;
using Orchard.UI.Admin;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPartDriver : ContentPartDriver<NotificationsPart> {
        private readonly INotificationsService _notificationsService;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        private const string NotificationsPlanName = "NotificationsPlan";

        public NotificationsPartDriver(
            INotificationsService notificationsService,
            IWorkContextAccessor workContextAccessor,
            IContentManager contentManager) {
            _notificationsService = notificationsService;
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }

        protected override string Prefix {
            get { return "SchedulingNotifications"; }
        }

        protected override DriverResult Display(NotificationsPart part, string displayType, dynamic shapeHelper) {
            var context = _workContextAccessor.GetContext();

            return Combined(
                ContentShape("Parts_NotificationsForm", () => {

                    // Check if user can subscribe for notifications
                    if (!_notificationsService.CanSubscribeForNotifications(part))
                        return null;

                    var user = context.CurrentUser;

                    // Already subscribed, can only check for authenticated users
                    var existingSubscription = user == null ? null : _notificationsService.GetSubscriptionsForEventAndUser(part.Id, user.Id).FirstOrDefault();

                    // Already subscribed, display the subscription shape
                    if (existingSubscription != null) {
                        return shapeHelper.Parts_NotificationsForm(
                            DisplayShape: _contentManager.BuildDisplay(existingSubscription, "Summary")
                        );
                    }

                    var notificationSubscription  = _contentManager.New<NotificationsSubscriptionPart>(Constants.NotificationsSubscriptionType);
                    notificationSubscription.Event = part.ContentItem;

                    // Create subscription editor shape
                    var editor = _contentManager.BuildEditor(notificationSubscription);

                    return shapeHelper.Parts_NotificationsForm(
                        EditorShape: editor
                    );
                }),
                ContentShape("Parts_NotificationsSubscriptionList_SummaryAdmin", () => {

                    if (!AdminFilter.IsApplied(context.HttpContext.Request.RequestContext))
                        return null;

                    var subscriptions = _notificationsService.GetSubscriptionsForEvent(part.Id).ToList();

                    // List subscriptions
                    var list = shapeHelper.List();
                    list.AddRange(subscriptions.Select(s => _contentManager.BuildDisplay(s, "SummaryAdmin")));

                    // TODO: pagination

                    return shapeHelper.Parts_NotificationsSubscriptionList(List: list, Count: subscriptions.Count);
                }));
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

            if (updater.TryUpdateModel(model, Prefix, null, new []{"NotificationPlans"})) {
                part.AllowNotifications = model.AllowNotifications;
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