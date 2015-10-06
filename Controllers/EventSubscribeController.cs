using System.Web.Mvc;
using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.Mvc.Extensions;
using Orchard.Themes;

namespace DQ.Scheduling.Controllers {
    [Themed]
    public class EventSubscribeController : Controller {
        private readonly ISubscriptionService _subscriptionService;
        public EventSubscribeController(ISubscriptionService subscriptionService) {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public ActionResult Subscribe(EventSubscribeViewModel model, string returnUrl) {            
            _subscriptionService.CreateSubscription(model);
            return this.RedirectLocal(returnUrl, "~/");
        }
    }
}