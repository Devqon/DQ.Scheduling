using Orchard.ContentManagement.Records;
using System;

namespace dsc.CalendarWidget.Models
{
    public class CalendarEventDefinitionRecord : ContentPartRecord
    {
        public virtual string TimeZone { get; set; }
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? EndDateTime { get; set; }
        public virtual bool IsAllDay { get; set; }
        public virtual string Url { get; set; }
        public virtual bool IsRecurring { get; set; }
    }
}
