using Orchard.Core.Common.ViewModels;

namespace DQ.Scheduling.ViewModels
{
    public class EventDefinitionEditViewModel
    {
        public DateTimeEditor StartDateTimeEditor { get; set; }
        public DateTimeEditor EndDateTimeEditor { get; set; }
        public bool AllDayEvent { get; set; }
        public bool IsRecurring { get; set; }
    }
}