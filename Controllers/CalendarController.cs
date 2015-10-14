using System;
using System.Web.Mvc;
using DQ.Scheduling.Helpers;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Controllers
{
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarController : Controller {
        private readonly IContentManager _contentManager;
        private readonly ICalendarService _calendarService;

        public CalendarController(IContentManager contentManager, ICalendarService calendarService) {
            _contentManager = contentManager;
            _calendarService = calendarService;
        }

        public ContentResult GetEvents(GetEventsViewModel viewModel) {

            // TODO: this part already assumes that the projection won't be used

            // Find provider by plugin name, take default if doesn't exists
            var provider = _calendarService.GetProviderOrDefault(viewModel.PluginName);

            var eventsQuery = _contentManager.Query<SchedulingPart, SchedulingPartRecord>();

            if (viewModel.Start.HasValue) {
                // Only events after given date
                eventsQuery = eventsQuery.Where(e => e.StartDateTime > viewModel.Start);
            }
            if (viewModel.End.HasValue) {
                // Only events before given date
                eventsQuery = eventsQuery.Where(e => e.EndDateTime <= viewModel.End);
            }

            var events = eventsQuery.Slice(0, viewModel.Take > 0 ? viewModel.Take : 100); // TODO: which number would be good?

            var formattedEvents = provider.FormatCalendarEvents(events);

            return new ContentResult {
                // Serialize to lowerCase
                Content = LowercaseJsonSerializer.SerializeObject(formattedEvents)
            };
        }
    }

    public class GetEventsViewModel {
        public string PluginName { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int Take { get; set; }
    }
}