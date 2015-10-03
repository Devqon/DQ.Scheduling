using System;
using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class CalendarEventDefinition : ContentPart<CalendarEventDefinitionRecord>
    {
        public string TimeZone {
            get { return Retrieve(x => x.TimeZone); }
            set { Store(x => x.TimeZone, value); }
        }

        public DateTime? StartDateTime
        {
            get { return Retrieve(x => x.StartDateTime); }
            set { Store(x => x.StartDateTime, value); }
        }

        public DateTime? EndDateTime
        {
            get { return Retrieve(x => x.EndDateTime); }
            set { Store(x => x.EndDateTime, value); }
        }

        public bool IsAllDay
        {
            get { return Retrieve(x => x.IsAllDay); }
            set { Store(x => x.IsAllDay, value); }
        }

        public string Url
        {
            get { return Retrieve(x => x.Url); }
            set { Store(x => x.Url, value); }
        }

        public bool IsRecurring
        {
            get { return Retrieve(x => x.IsRecurring); }
            set { Store(x => x.IsRecurring, value); }
        }

        public bool ShowTime
        {
            get { return !IsAllDay; }
        }
    }
}
