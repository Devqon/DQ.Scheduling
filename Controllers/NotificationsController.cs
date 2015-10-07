using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System.Web.Mvc;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Orchard.UI.Notify;

namespace DQ.Scheduling.Controllers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsController : Controller, IUpdateModel {
        private readonly INotificationsService _notificationsService;
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;

        public NotificationsController(
            INotificationsService notificationsService, 
            IContentManager contentManager, 
            IOrchardServices orchardServices) {
            _notificationsService = notificationsService;
            _contentManager = contentManager;
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        // TODO: complete implementation
        [HttpPost]
        public ActionResult Subscribe(string returnUrl) {

            var notificationSubscription = _contentManager.New<NotificationsSubscriptionPart>("NotificationSubscription");

            var editorShape = _contentManager.UpdateEditor(notificationSubscription, this);

            if (ModelState.IsValid) {
                _contentManager.Create(notificationSubscription);
            }
            else {
                _orchardServices.TransactionManager.Cancel();
            }

            // Ajax call
            if (Request.IsAjaxRequest()) {
                return Json(ModelState.IsValid ? "Success" : "Error", JsonRequestBehavior.DenyGet);
            }

            if (!ModelState.IsValid) {
                _orchardServices.Notifier.Error(T("Could not subscribe"));
                TempData["Scheduling.InvalidSubscriptionShape"] = editorShape;
            }

            return this.RedirectLocal(returnUrl, "~/");
        }

        // TODO: This should return JSON when Ajax call
        [HttpPost]
        public ActionResult UnSubscribe(int id, string returnUrl) {
            _notificationsService.DeleteSubscription(id);

            if (Request.IsAjaxRequest()) {
                return Json("Success", JsonRequestBehavior.DenyGet);
            }

            return this.RedirectLocal(returnUrl, "~/");
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}