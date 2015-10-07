using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using System.Web.Mvc;

namespace DQ.Scheduling.Controllers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsController : Controller {
        private readonly INotificationsService _notificationsService;
        public NotificationsController(INotificationsService notificationsService) {
            _notificationsService = notificationsService;
        }

        // TODO: This should return JSON when Ajax call
        [HttpPost]
        public ActionResult Subscribe(NotificationsFormEditViewModel model, string returnUrl) {            
            _notificationsService.CreateSubscription(model);
            return this.RedirectLocal(returnUrl, "~/");
        }

        // TODO: This should return JSON when Ajax call
        [HttpPost]
        public ActionResult UnSubscribe(int id, string returnUrl) {
            _notificationsService.DeleteSubscription(id);
            return this.RedirectLocal(returnUrl, "~/");
        }
    }
}