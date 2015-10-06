using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DQ.Scheduling.CalendarProviders {
    [OrchardFeature("DQ.FullCalendar")]
    public class FullCalendarProvider : ICalendarProvider {
        private readonly IContentManager _contentManager;
        private readonly UrlHelper _urlHelper;

        public FullCalendarProvider(IContentManager contentManager
            , UrlHelper urlHelper) {

            _contentManager = contentManager;
            _urlHelper = urlHelper;
        }

        public string Name { get { return "Fullcalendar"; } }

        public IEnumerable<SerializedEvent> SerializeEvents(IEnumerable<IContent> events) {
            var viewModels = new List<FullCalendarEventViewModel>();
            
            events.ToList().ForEach(ev => {
                var eventPart = ev.As<EventDefinitionPart>();

                var viewModel = new FullCalendarEventViewModel {
                    Id = ev.Id,
                    Title = _contentManager.GetItemMetadata(ev).DisplayText,
                    Start = eventPart.StartDateTime.GetValueOrDefault(),
                    End = eventPart.EndDateTime.GetValueOrDefault(),
                    Url = _urlHelper.ItemDisplayUrl(ev),
                    AllDay = eventPart.IsAllDay
                };

                viewModels.Add(viewModel);
            });

            return viewModels;
        }
    }
}