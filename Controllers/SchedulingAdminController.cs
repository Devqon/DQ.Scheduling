using System.Linq;
using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;

namespace DQ.Scheduling.Controllers
{
    [Admin]
    [ValidateInput(false)]
    public class SchedulingAdminController : Controller {

        private readonly ISchedulingService _schedulingService;
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;
        private readonly INotificationsService _notificationsService;

        public SchedulingAdminController(
            ISchedulingService schedulingService, 
            IShapeFactory shapeFactory, 
            ISiteService siteService, 
            IContentManager contentManager, 
            INotificationsService notificationsService) {

            _schedulingService = schedulingService;
            Shape = shapeFactory;
            _siteService = siteService;
            _contentManager = contentManager;
            _notificationsService = notificationsService;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }
        dynamic Shape { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);

            var eventsQuery = _contentManager.Query<SchedulingPart, SchedulingPartRecord>();

            var pagerShape = Shape.Pager(pager).TotalItemCount(eventsQuery.Count());
            var entries = eventsQuery
                .OrderBy<SchedulingPartRecord>(cpr => cpr.StartDateTime)
                .Slice(pager.GetStartIndex(), pager.PageSize)
                .ToList()
                .Select(CreateSchedulingEntry);

            var model = new SchedulingIndexViewModel {
                SchedulingEntries = entries.ToList(),
                Pager = pagerShape
            };

            return View(model);
        }

        private SchedulingEntry CreateSchedulingEntry(SchedulingPart part) {
            var subscriptions = _notificationsService.GetSubscriptionsForEvent(part.Id).ToList();

            return new SchedulingEntry {
                Part = part,
                Start = part.StartDateTime.GetValueOrDefault(),
                End = part.EndDateTime.GetValueOrDefault(),
                IsAllDay = part.IsAllDay,
                IsRecurring = part.IsRecurring,
                Subscriptions = subscriptions.Count
            };
        }
    }
}