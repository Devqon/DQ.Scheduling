using System;
using System.Collections.Generic;
using DQ.Scheduling.Models;

namespace DQ.Scheduling.ViewModels {
    public class SchedulingIndexViewModel {
        public IList<SchedulingEntry> SchedulingEntries { get; set; }
        public dynamic Pager { get; set; }
    }

    public class SchedulingEntry {
        public SchedulingPart Part { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsRecurring { get; set; }
        public int Subscriptions { get; set; }
    }
}