using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using System;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.Scheduling")]
    public class EventDefinitionPartRecord : ContentPartRecord {
        public virtual string TimeZone { get; set; }
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? EndDateTime { get; set; }
        public virtual bool IsAllDay { get; set; }
        public virtual bool IsRecurring { get; set; }
    }
}
