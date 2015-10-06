using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSubscribePartRecord : ContentPartRecord {
        public virtual bool AllowSubscriptions { get; set; }
    }
}