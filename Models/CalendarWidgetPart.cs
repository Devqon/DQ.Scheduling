using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class CalendarWidgetPart : ContentPart<CalendarWidgetPartRecord>
    {
        public int QueryId
        {
            get { return Record.QueryId; }
            set { Record.QueryId = value; }
        }
    }
}