﻿using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers
{
    [OrchardFeature("DQ.CalendarWidget")]
    public class CalendarWidgetDriver:ContentPartDriver<CalendarWidgetPart>
    {
        private readonly ICalendarService _calendarService;
        private readonly IEventService _eventService;

        public Localizer T { get; set; }

        public CalendarWidgetDriver(ICalendarService calendarService, IEventService eventService)
        {
            _calendarService = calendarService;
            _eventService = eventService;

            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(CalendarWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CalendarWidget",
            	() => shapeHelper.Parts_CalendarWidget(
                	CalendarEvents: _calendarService.GetEventDefinitions(part),
                    Plugin: part.Plugin
                )
			);
        }

        protected override DriverResult Editor(CalendarWidgetPart part, dynamic shapeHelper)
        {
            var model = new EditCalendarWidgetViewModel
            {
                Queries = _eventService.GetEventDefinitionQueries(),
                QueryId = part.QueryId,
                Plugins = _calendarService.GetCalendarPlugins(),
                Plugin = part.Plugin
            };

            return ContentShape("Parts_CalendarWidget_Edit",
            	() => shapeHelper.EditorTemplate(
            		TemplateName: "Parts/CalendarWidget",
                	Model: model,
            		Prefix: Prefix
            	)
            );
        }

        protected override DriverResult Editor(CalendarWidgetPart part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = new EditCalendarWidgetViewModel();

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