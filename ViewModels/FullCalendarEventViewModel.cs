using DQ.Scheduling.CalendarProviders;
using Orchard.Environment.Extensions;
using System;

namespace DQ.Scheduling.ViewModels {
    [OrchardFeature("DQ.FullCalendar")]
    public class FullCalendarEventViewModel : SerializedEvent {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Url { get; set; }
    }
}