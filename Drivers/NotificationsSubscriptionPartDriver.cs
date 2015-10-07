using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers
{
    public class NotificationsSubscriptionPartDriver : ContentPartDriver<NotificationsSubscriptionPart> {

        public NotificationsSubscriptionPartDriver() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Editor(NotificationsSubscriptionPart part, dynamic shapeHelper) {
            return ContentShape("Parts_NotificationsSubscription_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/NotificationsSubscription",
                Model: part
            ));
        }

        protected override DriverResult Editor(NotificationsSubscriptionPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);

            if (part.SubscribeType == SubscribeType.Email || part.SubscribeType == SubscribeType.Both && string.IsNullOrEmpty(part.Email)) {
                updater.AddModelError("Email", T("Email is mandatory"));
            }
            if (part.SubscribeType == SubscribeType.Sms || part.SubscribeType == SubscribeType.Both && string.IsNullOrEmpty(part.Phone)) {
                updater.AddModelError("Email", T("Phone number is mandatory"));
            }

            return Editor(part, shapeHelper);
        }
    }
}