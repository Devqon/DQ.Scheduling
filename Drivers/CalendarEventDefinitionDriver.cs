using System;
using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.ViewModels;
using Orchard.Localization;
using Orchard.Localization.Services;

namespace DQ.Scheduling.Drivers
{
    public class CalendarEventDefinitionDriver : ContentPartDriver<CalendarEventDefinition> {
        private readonly IDateLocalizationServices _dateLocalizationServices;
        public CalendarEventDefinitionDriver(IDateLocalizationServices dateLocalizationServices) {
            _dateLocalizationServices = dateLocalizationServices;

            T = NullLocalizer.Instance;
        }

        private const string TemplateName = "Parts/CalendarEventDefinition";

        public Localizer T { get; set; }

        protected override string Prefix
        {
            get { return "CalendarEventDefinition"; }
        }

        protected override DriverResult Display(CalendarEventDefinition part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_CalendarEventDefinition",
                () => shapeHelper.Parts_CalendarEventDefinition(part));
        }

        protected override DriverResult Editor(CalendarEventDefinition part, dynamic shapeHelper) {
            var viewModel = BuildViewModelFromPart(part);

            return ContentShape("Parts_CalendarEventDefinition_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: viewModel, Prefix: Prefix));
        }

        protected override DriverResult Editor(CalendarEventDefinition part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = BuildViewModelFromPart(part);

            if (updater.TryUpdateModel(viewModel, Prefix, null, null)) {
                try {
                    // Start
                    var utcStartDateTime = viewModel.AllDayEvent 
                        ? _dateLocalizationServices.ConvertFromLocalizedDateString(viewModel.StartDateTimeEditor.Date) 
                        : _dateLocalizationServices.ConvertFromLocalizedString(viewModel.StartDateTimeEditor.Date, viewModel.StartDateTimeEditor.Time);
                    part.StartDateTime = utcStartDateTime;
                }
                catch (FormatException) {
                    updater.AddModelError(Prefix, T("'{0} {1}' could not be parsed as a valid date and time.", viewModel.StartDateTimeEditor.Date, viewModel.StartDateTimeEditor.Time));
                }
                try
                {
                    // End
                    var utcEndDateTime = _dateLocalizationServices.ConvertFromLocalizedString(viewModel.EndDateTimeEditor.Date, viewModel.EndDateTimeEditor.Time);
                    part.EndDateTime = utcEndDateTime;
                }
                catch (FormatException)
                {
                    updater.AddModelError(Prefix, T("'{0} {1}' could not be parsed as a valid date and time.", viewModel.EndDateTimeEditor.Date, viewModel.EndDateTimeEditor.Time));
                }
            }

            return ContentShape("Parts_CalendarEventDefinition_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: viewModel, Prefix: Prefix));
        }

        private CalendarEventEditViewModel BuildViewModelFromPart(CalendarEventDefinition part)
        {
            return new CalendarEventEditViewModel
            {
                AllDayEvent = part.IsAllDay,
                IsRecurring = part.IsRecurring,
                StartDateTimeEditor = new DateTimeEditor {
                    ShowDate = true,
                    ShowTime = true,
                    Date = _dateLocalizationServices.ConvertToLocalizedDateString(part.StartDateTime),
                    Time = _dateLocalizationServices.ConvertToLocalizedTimeString(part.StartDateTime)
                },
                EndDateTimeEditor = new DateTimeEditor {
                    ShowDate = true,
                    ShowTime = true,
                    Date = _dateLocalizationServices.ConvertToLocalizedDateString(part.EndDateTime),
                    Time = _dateLocalizationServices.ConvertToLocalizedTimeString(part.EndDateTime)
                }
            };
        }
    }
}