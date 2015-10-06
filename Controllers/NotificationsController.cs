using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using System.Web.Mvc;

namespace DQ.Scheduling.Controllers {
    [Themed]
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsController : Controller {
        private readonly INotificationsService _subscriptionService;
        public NotificationsController(INotificationsService subscriptionService) {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public ActionResult Subscribe(NotificationsEditViewModel model, string returnUrl) {            
            _subscriptionService.CreateSubscription(model);
            return this.RedirectLocal(returnUrl, "~/");
        }

        [HttpPost]
        public ActionResult UnSubscribe(int id, string returnUrl) {
            
            _subscriptionService.DeleteSubscription(id);

            return this.RedirectLocal(returnUrl, "~/");
        }
    }
}