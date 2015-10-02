using System.Collections.Generic;
using Orchard.Projections.Models;

namespace dsc.CalendarWidget.ViewModels
{
    public class CalendarWidgetPartQueries
    {
        public IEnumerable<QueryPart> Queries { get; set; }
        public int QueryId { get; set; }
    }
}