using DQ.Scheduling.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Projections.Models;
using Orchard.Projections.Services;
using Orchard.Tasks.Scheduling;
using System.Collections.Generic;
using System.Linq;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingService : ISchedulingService {
        private readonly IOrchardServices _orchardServices;
        private readonly IProjectionManager _projectionManager;
        private readonly IScheduledTaskManager _scheduledTaskManager;

        public SchedulingService(IOrchardServices orchardServices, IProjectionManager projectionManager, IScheduledTaskManager scheduledTaskManager) {
            _orchardServices = orchardServices;
            _projectionManager = projectionManager;
            _scheduledTaskManager = scheduledTaskManager;
        }

        // TODO: use something else than query because Queries do not take permissions in account
        public List<QueryPart> GetEventDefinitionQueries() {
            var queryParts = _orchardServices.ContentManager.Query<QueryPart, QueryPartRecord>("Query").List();
            var eventDefinitionQueries = new List<QueryPart>();

            foreach (var part in queryParts) {
                //var contentItem = _projectionManager.GetContentItems(part.Id).FirstOrDefault();
                //if (contentItem == null) {
                //    return new List<QueryPart>();
                //}

                //// Check if has Event Definition part
                //if (contentItem.TypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof (SchedulingPart).Name)) {
                    eventDefinitionQueries.Add(part);
                //}
            }

            return eventDefinitionQueries;
        }

        public void ScheduleEvent(SchedulingPart eventDefinitionPart) {
            // Delete ongoing schedules
            // TODO: also the other taskTypes, see Constants -> Scheduling Constants
            _scheduledTaskManager.DeleteTasks(eventDefinitionPart.ContentItem, task => task.TaskType == Constants.EventStartedName);

            if (eventDefinitionPart.StartDateTime.HasValue) {
                _scheduledTaskManager.CreateTask(Constants.EventStartedName, eventDefinitionPart.StartDateTime.Value, eventDefinitionPart.ContentItem);
            }
        }
    }
}