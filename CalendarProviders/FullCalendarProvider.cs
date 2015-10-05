using System.Collections.Generic;
using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Html;

namespace DQ.Scheduling.CalendarProviders
{
    [OrchardFeature("DQ.FullCalendar")]
    public class FullCalendarProvider : ICalendarProvider
    {
        private readonly IOrchardServices _orchardServices;
        public FullCalendarProvider(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public string Name { get { return "Fullcalendar"; } }

        public IEnumerable<SerializedEvent> SerializeEvents(IEnumerable<IContent> events) {
            var viewModels = new List<EventDefinitionViewModel>();

            var currentContext = _orchardServices.WorkContext;
            var urlHelper = new UrlHelper(currentContext.HttpContext.Request.RequestContext);

            foreach (var ci in events)
            {
                var eventPart = ci.As<EventDefinitionPart>();

                var viewModel = new EventDefinitionViewModel
                {
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