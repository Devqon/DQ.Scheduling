using Orchard.ContentManagement.Records;

namespace DQ.Scheduling.Models
{
    public class EventSubscribePartRecord : ContentPartRecord
    {
        public virtual bool AllowSubscriptions { get; set; }
    }
}