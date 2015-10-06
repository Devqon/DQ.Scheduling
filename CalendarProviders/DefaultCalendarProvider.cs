using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System.Collections.Generic;

namespace DQ.Scheduling.CalendarProviders {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class DefaultCalendarProvider : ICalendarProvider {

        private readonly IContentManager _contentManager;
        public DefaultCalendarProvider(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public string Name { get { return "Default"; } }

        public IEnumerable<SerializedEvent> SerializeEvents(IEnumerable<IContent> events) {
            var viewModels = new List<CalendarDefaultDisplayViewModel>();

            foreach (var ci in events) {
                var eventPart = ci.As<SchedulingPart>();
                var viewModel = new CalendarDefaultDisplayViewModel {
                    Title = _contentManager.GetItemMetadata(ci).DisplayText,
                    Start = eventPart.StartDateTime.GetValueOrDefault(),
                    End = eventPart.EndDateTime.GetValueOrDefault(),
                    Event = ci
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}