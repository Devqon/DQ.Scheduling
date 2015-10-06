using Orchard.Core.Common.ViewModels;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.Scheduling")]
    public class EventDefinitionEditViewModel {
        public DateTimeEditor StartDateTimeEditor { get; set; }
        public DateTimeEditor EndDateTimeEditor { get; set; }
        public bool AllDayEvent { get; set; }
        public bool IsRecurring { get; set; }
    }
}