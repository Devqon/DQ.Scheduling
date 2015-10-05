using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using Orchard.ContentManagement;
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
            var viewModels = new List<FullCalendarEventViewModel>();

            var currentContext = _orchardServices.WorkContext;
            var urlHelper = new UrlHelper(currentContext.HttpContext.Request.RequestContext);

            events.ToList().ForEach(ev => {
                var eventPart = ev.As<EventDefinitionPart>();

                var viewModel = new FullCalendarEventViewModel
                {
                    Id = ev.Id,
                    Title = _orchardServices.ContentManager.GetItemMetadata(ev).DisplayText,
                    Start = eventPart.StartDateTime.GetValueOrDefault(),
                    End = eventPart.EndDateTime.GetValueOrDefault(),
                    Url = urlHelper.ItemDisplayUrl(ev),
                    AllDay = eventPart.IsAllDay
                };

                viewModels.Add(viewModel);
            });

            return viewModels;
        }
    }
}