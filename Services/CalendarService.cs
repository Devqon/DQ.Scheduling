using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Mvc.Html;
using Orchard.Projections.Models;
using Orchard.Projections.Services;
using Orchard.Utility.Extensions;

namespace DQ.Scheduling.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IProjectionManager _projectionManager;
        private readonly IOrchardServices _orchardServices;
        private readonly ICacheManager _cacheManager;
        private readonly Func<IShapeTableLocator> _shapeTableLocator;
        private readonly IWorkContextAccessor _wca;

        public CalendarService(IProjectionManager projectionManager, IOrchardServices orchardServices, ICacheManager cacheManager, Func<IShapeTableLocator> shapeTableLocator, IWorkContextAccessor wca)
        {
            _projectionManager = projectionManager;
            _orchardServices = orchardServices;
            _cacheManager = cacheManager;
            _shapeTableLocator = shapeTableLocator;
            _wca = wca;
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

                // Check if has calendareventdefinition part
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

        public IList<string> GetCalendarPlugins() {
            return _cacheManager.Get("CalendarPlugins", context =>
            {
                var shapeTable = _shapeTableLocator().Lookup(_wca.GetContext().CurrentTheme.Id);
                // Look up CalendarWidget-{plugin}.cshtml shapes to define which plugins are available
                // taken from Orchard.Core.Common.Services.FlavorService
                var plugins = shapeTable.Bindings.Keys
                    .Where(x => x.StartsWith("Parts_CalendarWidget__", StringComparison.OrdinalIgnoreCase))
                    .Select(x => x.Substring("Parts_CalendarWidget__".Length))
                    .Where(x => !String.IsNullOrWhiteSpace(x))
                    .Select(x => x[0].ToString(CultureInfo.InvariantCulture).ToUpper() + x.Substring(1))
                    .Select(x => x.CamelFriendly());

                return plugins.ToList();
            });
        } 
    }
}