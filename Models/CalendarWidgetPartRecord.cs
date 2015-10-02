using Orchard.ContentManagement.Records;

namespace dsc.CalendarWidget.Models
{
    public class CalendarWidgetPartRecord : ContentPartRecord
    {
        public virtual int QueryId { get; set; }
    }
}