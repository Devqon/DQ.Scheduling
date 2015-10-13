using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using System;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingPartRecord : ContentPartRecord {
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? EndDateTime { get; set; }
        public virtual bool IsAllDay { get; set; }
        public virtual bool IsRecurring { get; set; }
        public virtual string DisplayUrlOverride { get; set; }
    }
}
