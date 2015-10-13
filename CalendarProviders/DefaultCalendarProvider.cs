using System.Web.Mvc;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System.Collections.Generic;
using Orchard.Mvc.Html;

namespace DQ.Scheduling.CalendarProviders {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class DefaultCalendarProvider : ICalendarProvider {

        private readonly IContentManager _contentManager;
        private readonly UrlHelper _urlHelper;

        public DefaultCalendarProvider(IContentManager contentManager, UrlHelper urlHelper) {
            _contentManager = contentManager;
            _urlHelper = urlHelper;
        }

        public string Name { get { return "Default"; } }

        public IEnumerable<FormattedEvent> FormatCalendarEvents(IEnumerable<IContent> events) {
            var viewModels = new List<CalendarDefaultDisplayViewModel>();

            foreach (var ci in events) {
                var eventPart = ci.As<SchedulingPart>();
                var viewModel = new CalendarDefaultDisplayViewModel {
                    Title = _contentManager.GetItemMetadata(ci).DisplayText,
                    Start = eventPart.StartDateTime.GetValueOrDefault(),
                    End = eventPart.EndDateTime.GetValueOrDefault(),
                    DisplayUrl = string.IsNullOrWhiteSpace(eventPart.DisplayUrlOverride) ? _urlHelper.ItemDisplayUrl(ci) : eventPart.DisplayUrlOverride
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}