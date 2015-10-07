using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsPlanPartDriver : ContentPartDriver<NotificationsPlanPart> {
        private readonly IContentManager _contentManager;

        public NotificationsPlanPartDriver(IContentManager contentManager) {
            _contentManager = contentManager;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix {
            get { return "SchedulingNotificationsPlan"; }
        }

        protected override DriverResult Display(NotificationsPlanPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_NotificationsPlan",
                () => shapeHelper.Parts_NotificationsPlan(
                    NotificationsPlan: part));
        }

        //GET
        protected override DriverResult Editor(NotificationsPlanPart part, dynamic shapeHelper) {
            return ContentShape("Parts_NotificationsPlan_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/NotificationsPlan",
                Model: part,
                Prefix: Prefix));
        }

        //POST
        protected override DriverResult Editor(NotificationsPlanPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(NotificationsPlanPart part, ImportContentContext context) {
            var el = context.Data.Element(typeof(NotificationsPlanPart).Name);
            if (el == null) return;
            el.With(part)
                .FromAttr(p => p.UpcomingNotificationInterval)
                .FromAttr(p => p.UpcomingNotificationIntervalCount)
                .FromAttr(p => p.FollowUpNotificationInterval)
                .FromAttr(p => p.FollowUpNotificationIntervalCount);
        }

        protected override void Exporting(NotificationsPlanPart part, ExportContentContext context) {
            var el = context.Element(typeof(NotificationsPlanPart).Name);
            el.With(part)
                .ToAttr(p => p.UpcomingNotificationInterval)
                .ToAttr(p => p.UpcomingNotificationIntervalCount)
                .ToAttr(p => p.FollowUpNotificationInterval)
                .ToAttr(p => p.FollowUpNotificationIntervalCount);
        }

    }
}