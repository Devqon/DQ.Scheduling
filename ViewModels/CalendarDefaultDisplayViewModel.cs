using DQ.Scheduling.CalendarProviders;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarDefaultDisplayViewModel : SerializedEvent {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IContent Event { get; set; }
    }
}