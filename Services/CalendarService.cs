using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Mvc.Html;
using Orchard.Projections.Models;
using Orchard.Projections.Services;

namespace DQ.Scheduling.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IProjectionManager _projectionManager;
        private readonly IOrchardServices _orchardServices;

        public CalendarService(IProjectionManager projectionManager, IOrchardServices orchardServices)
        {
            _projectionManager = projectionManager;
            _orchardServices = orchardServices;
        }

        public List<QueryPart> GetCalendarQueries()
        {
            var queryParts = _orchardServices.ContentManager.Query<QueryPart, QueryPartRecord>("Query").List();

            var calendarQueries = new List<QueryPart>();

            foreach (QueryPart part in queryParts)
            {
                var contentItem = _projectionManager.GetContentItems(part.Id).FirstOrDefault();
                if (contentItem == null) {
                    return new List<QueryPart>();
                }

                var countCalendarEventDefinition = contentItem.TypeDefinition.Parts.Count(r => r.PartDefinition.Name == "CalendarEventDefinition");

                if (countCalendarEventDefinition > 0)
                {
                    calendarQueries.Add(part);
                }
            }

            return calendarQueries;
        }

        public IEnumerable<CalendarEventViewModel> GetCalendarEvents(CalendarWidgetPart part)
        {
            var contentItems = _projectionManager.GetContentItems(part.QueryId);

            var viewModels = new List<CalendarEventViewModel>();

            var currentContext = _orchardServices.WorkContext;
            var urlHelper = new UrlHelper(currentContext.HttpContext.Request.RequestContext);

            foreach (var ci in contentItems) {
                var eventPart = ci.As<CalendarEventDefinition>();

                var viewModel = new CalendarEventViewModel {
                    Id = ci.Id,
                    Title = ci.As<TitlePart>().Title,
                    Start = eventPart.StartDateTime.GetValueOrDefault(),
                    End = eventPart.EndDateTime.GetValueOrDefault(),
                    Url = urlHelper.ItemDisplayUrl(ci),
                    AllDay = eventPart.IsAllDay
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}