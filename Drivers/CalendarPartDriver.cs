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
            return ContentShape("Parts_Calendar",
            	() => shapeHelper.Parts_Calendar(
                	CalendarEvents: _calendarService.GetEventDefinitions(part),
                    Plugin: part.Plugin
                )
			);
        }

        protected override DriverResult Editor(CalendarPart part, dynamic shapeHelper) {
            var model = new CalendarEditViewModel {
                Queries = _eventService.GetEventDefinitionQueries(),
                QueryId = part.QueryId,
                Plugins = _calendarService.GetCalendarPlugins(),
                Plugin = part.Plugin
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
                part.Plugin = viewModel.Plugin;
                part.QueryId = viewModel.QueryId;
                if (part.QueryId <= 0) {
                    updater.AddModelError("QueryId", T("You must select a query."));
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}