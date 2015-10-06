using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tasks.Scheduling;
using Orchard.Workflows.Services;
using System.Collections.Generic;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingWorkflows")]
    public class EventWorkflowsHandler : IScheduledTaskHandler {
        private readonly IWorkflowManager _workflowManager;

        public EventWorkflowsHandler(IWorkflowManager workflowManager) {
            _workflowManager = workflowManager;
        }

        public void Process(ScheduledTaskContext context) {
            if (context.Task.TaskType != Constants.EventStartedName)
                return;

            var contentItem = context.Task.ContentItem;
            var eventDefinition = contentItem.As<SchedulingPart>();
            if (eventDefinition == null)
                return;

            // Trigger workflow event
            _workflowManager.TriggerEvent(Constants.EventStartedName,
                contentItem,
                () => new Dictionary<string, object>{
                    { "StartDateTime", eventDefinition.StartDateTime },
                    { "EndDateTime", eventDefinition.EndDateTime },
                    { "Content", contentItem }
                });
        }
    }
}