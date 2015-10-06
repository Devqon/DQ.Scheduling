using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.ViewModels;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Localization.Services;
using System;

namespace DQ.Scheduling.Drivers {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingPartDriver : ContentPartDriver<SchedulingPart> {
        private readonly IDateLocalizationServices _dateLocalizationServices;
        public SchedulingPartDriver(IDateLocalizationServices dateLocalizationServices) {
            _dateLocalizationServices = dateLocalizationServices;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        private const string TemplateName = "Parts/Scheduling";
        
        protected override string Prefix {
            get { return "Scheduling"; }
        }

        protected override DriverResult Display(SchedulingPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Scheduling",
                () => shapeHelper.Parts_Scheduling(part));
        }

        protected override DriverResult Editor(SchedulingPart part, dynamic shapeHelper) {
            var viewModel = BuildViewModelFromPart(part);

            return ContentShape("Parts_Scheduling_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: viewModel, Prefix: Prefix));
        }

        protected override DriverResult Editor(SchedulingPart part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = BuildViewModelFromPart(part);

            if (updater.TryUpdateModel(viewModel, Prefix, null, null)) {
                part.IsAllDay = viewModel.AllDayEvent;
                part.IsRecurring = viewModel.IsRecurring;
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
                try {
                    // End
                    var utcEndDateTime = _dateLocalizationServices.ConvertFromLocalizedString(viewModel.EndDateTimeEditor.Date, viewModel.EndDateTimeEditor.Time);
                    part.EndDateTime = utcEndDateTime;
                }
                catch (FormatException)
                {
                    updater.AddModelError(Prefix, T("'{0} {1}' could not be parsed as a valid date and time.", viewModel.EndDateTimeEditor.Date, viewModel.EndDateTimeEditor.Time));
                }
            }

            return ContentShape("Parts_Scheduling_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: viewModel, Prefix: Prefix));
        }

        private SchedulingEditViewModel BuildViewModelFromPart(SchedulingPart part) {
            return new SchedulingEditViewModel {
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