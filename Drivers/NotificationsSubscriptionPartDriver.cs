using DQ.Scheduling.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers
{
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPartDriver : ContentPartDriver<NotificationsSubscriptionPart> {

        private readonly IWorkContextAccessor _workContextAccessor;

        public NotificationsSubscriptionPartDriver(IWorkContextAccessor workContextAccessor) {
            _workContextAccessor = workContextAccessor;
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

                // TODO: use subscribe type
                part.SubscribeType = SubscribeType.Email;
                var currentUser = _workContextAccessor.GetContext().CurrentUser;
                if (currentUser == null) {
                    // Email should be filled in
                    if (string.IsNullOrEmpty(part.Email)) {
                        updater.AddModelError("Email", T("Email is mandatory"));
                    }
                }
                else {
                    part.Email = currentUser.Email;
                    part.UserId = currentUser.Id;
                }
            }
            if (part.SubscribeType == SubscribeType.Sms || part.SubscribeType == SubscribeType.Both && string.IsNullOrEmpty(part.Phone)) {
                updater.AddModelError("Email", T("Phone number is mandatory"));
            }

            return Editor(part, shapeHelper);
        }
    }
}