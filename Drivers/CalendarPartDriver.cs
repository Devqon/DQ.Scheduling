using System.Collections.Generic;
using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Helpers;
using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarPartDriver:ContentPartDriver<CalendarPart> {
        private readonly ICalendarService _calendarService;
        private readonly ISchedulingService _eventService;
        
        public CalendarPartDriver(ICalendarService calendarService, ISchedulingService eventService) {
            _calendarService = calendarService;
            _eventService = eventService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CalendarPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Calendar", () => {

                IEnumerable<FormattedEvent> events = null;

                // Do not initialize events if ajax is to be used
                var initializeEvents = !part.UseAsync || !_calendarService.GetProviderOrDefault(part.Plugin).SupportsAjax;

                // If don't use async or plugin doesn't support async, get formatted events
                if (initializeEvents) {
                    events = _calendarService.GetFormattedCalendarEvents(part);
                }

                return shapeHelper.Parts_Calendar(
                    CalendarEvents: events,
                    Plugin: part.Plugin,
                    UseAsync: !initializeEvents
                );
            });
        }

        protected override DriverResult Editor(CalendarPart part, dynamic shapeHelper) {
            var model = new CalendarEditViewModel {
                Queries = _eventService.GetEventDefinitionQueries(),
                QueryId = part.QueryId,
                Plugins = _calendarService.GetProviders(),
                Plugin = part.Plugin,
                UseAsync = part.UseAsync
            };

            return ContentShape("Parts_Calendar_Edit",
            	() => shapeHelper.EditorTemplate(
            		TemplateName: "Parts/Calendar",
                	Model: model,
            		Prefix: Prefix
            	)
            );
        }

        protected override DriverResult Editor(CalendarPart part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = new CalendarEditViewModel();

            if (updater.TryUpdateModel(viewModel, Prefix, null, new[] {"Queries", "Plugins"})) {
                part.Plugin = string.IsNullOrEmpty(viewModel.Plugin) ? Constants.DefaultCalendarName : viewModel.Plugin;
                part.QueryId = viewModel.QueryId;
                part.UseAsync = viewModel.UseAsync && _calendarService.GetProviderOrDefault(viewModel.Plugin).SupportsAjax;

                if (part.QueryId <= 0) {
                    updater.AddModelError("QueryId", T("You must select a query."));
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}