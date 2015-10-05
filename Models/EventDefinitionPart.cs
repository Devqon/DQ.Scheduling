using System;
using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class EventDefinitionPart : ContentPart<EventDefinitionPartRecord>
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
