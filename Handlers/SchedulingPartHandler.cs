using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingPartHandler : ContentHandler {
        public SchedulingPartHandler(IRepository<SchedulingPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}