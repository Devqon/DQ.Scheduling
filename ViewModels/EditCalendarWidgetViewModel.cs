using System.Collections.Generic;
using Orchard.Projections.Models;

namespace DQ.Scheduling.ViewModels
{
    public class EditCalendarWidgetViewModel
    {
        public IEnumerable<QueryPart> Queries { get; set; }
        public int QueryId { get; set; }
        public IEnumerable<string> Plugins { get; set; }
        public string Plugin { get; set; }
    }
}