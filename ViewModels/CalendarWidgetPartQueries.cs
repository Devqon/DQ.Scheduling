using System.Collections.Generic;
using Orchard.Projections.Models;

namespace DQ.Scheduling.ViewModels
{
    public class CalendarWidgetPartQueries
    {
        public IEnumerable<QueryPart> Queries { get; set; }
        public int QueryId { get; set; }
    }
}