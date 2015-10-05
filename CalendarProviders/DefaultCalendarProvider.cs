using System.Collections.Generic;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;

namespace DQ.Scheduling.CalendarProviders
{
    public class DefaultCalendarProvider : ICalendarProvider {

        public string Name { get { return "Default"; } }

        public IEnumerable<SerializedEvent> SerializeEvents(IEnumerable<IContent> events) {
            var viewModels = new List<DefaultCalendarEventViewModel>();

            foreach (var ci in events)
            {
                var eventPart = ci.As<EventDefinitionPart>();

                var viewModel = new DefaultCalendarEventViewModel
                {
                    Title = ci.As<TitlePart>().Title,
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