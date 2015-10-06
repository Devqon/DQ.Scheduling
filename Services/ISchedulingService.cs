using System.Collections.Generic;
using DQ.Scheduling.Models;
using Orchard;
using Orchard.Projections.Models;

namespace DQ.Scheduling.Services {
    public interface ISchedulingService : IDependency {
        /// <summary>
        /// Get all projection queries that have content items with an event definition part
        /// </summary>
        /// <returns></returns>
        List<QueryPart> GetEventDefinitionQueries();

        /// <summary>
        /// Schedule the event for start
        /// </summary>
        /// <param name="eventDefinitionPart"></param>
        void ScheduleEvent(SchedulingPart eventDefinitionPart);
    }
}
