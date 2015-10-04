using Orchard.ContentManagement;

namespace DQ.Scheduling.Models
{
    public class CalendarWidgetPart : ContentPart<CalendarWidgetPartRecord>
    {
        public int QueryId
        {
            get { return Retrieve(x => x.QueryId); }
            set { Store(x => x.QueryId, value); }
        }

        public string Plugin {
            get { return Retrieve(x => x.Plugin); }
            set { Store(x => x.Plugin, value); }
        }
    }
}