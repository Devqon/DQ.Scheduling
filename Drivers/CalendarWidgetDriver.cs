using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;

namespace DQ.Scheduling.Drivers
{
    public class CalendarWidgetDriver:ContentPartDriver<CalendarWidgetPart>
    {
        private readonly ICalendarService _calendarService;

        public Localizer T { get; set; }

        public CalendarWidgetDriver(ICalendarService calendarService)
        {
            _calendarService = calendarService;

            T = NullLocalizer.Instance;
        }

        protected override DriverResult Display(CalendarWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CalendarWidget",
            	() => shapeHelper.Parts_CalendarWidget(
                	CalendarEvents: _calendarService.GetCalendarEvents(part)
                )
			);
        }

        protected override DriverResult Editor(CalendarWidgetPart part, dynamic shapeHelper)
        {
            var model = new CalendarWidgetPartQueries
            {
                Queries = _calendarService.GetCalendarQueries(),
                QueryId = part.QueryId
            };

            return ContentShape("Parts_CalendarWidget_Edit",
            	() => shapeHelper.EditorTemplate(
            		TemplateName: "Parts/CalendarWidget",
                	Model: model,
            		Prefix: Prefix
            	)
            );
        }

        protected override DriverResult Editor(CalendarWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, new [] { "Queries" });

            if (part.QueryId <= 0)
            {
                updater.AddModelError("QueryId", T("You must select a query."));
            }

            return Editor(part, shapeHelper);
        }
    }
}