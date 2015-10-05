using System;
using DQ.Scheduling.CalendarProviders;
using Orchard.ContentManagement;

namespace DQ.Scheduling.ViewModels
{
    public class DefaultCalendarEventViewModel : SerializedEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IContent Event { get; set; }
    }
}