using System.Linq;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Settings.Models;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using System.Web.Mvc;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Orchard.Themes;
using Orchard.UI.Navigation;
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
            IOrchardServices orchardServices,
            IShapeFactory shapeFactory) {

            _notificationsService = notificationsService;
            _contentManager = contentManager;
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        public Localizer T { get; set; }
        public dynamic Shape { get; set; }

        // TODO: complete implementation
        [HttpPost]
        public ActionResult Subscribe(string returnUrl) {

            var notificationSubscription = _contentManager.New(Constants.NotificationsSubscriptionType);

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

        // 'My' subscriptions
        [HttpGet]
        [Themed]
        public ActionResult Subscriptions(PagerParameters pagerParameters) {

            var user = _orchardServices.WorkContext.CurrentUser;
            // Cannot get subscriptions for unknown user
            if(user == null)
                return new HttpNotFoundResult();

            var pager = new Pager(_orchardServices.WorkContext.CurrentSite.As<SiteSettingsPart>(), pagerParameters);

            var query = _notificationsService
                .GetNotificationsSubscriptionQuery()
                .Where(s => s.UserId == user.Id);

            var count = query.Count();

            var pagedSubscriptions = query
                .Slice(pager.GetStartIndex(), pager.PageSize)
                .ToList();

            // Build subscription shapes
            var subscriptionShapes = pagedSubscriptions.Select(p => _contentManager.BuildDisplay(p, "Summary"));

            var list = Shape.List();
            list.AddRange(subscriptionShapes);

            var viewModel = Shape
                .ViewModel()
                .List(list)
                .Pager(Shape.Pager(pager))
                .TotalItemCount(count);

            return View(viewModel);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}