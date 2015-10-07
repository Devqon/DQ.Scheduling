using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Tasks.Scheduling;
using Orchard.Workflows.Services;
using System.Collections.Generic;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsScheduledTaskHandler : IScheduledTaskHandler {
        private readonly IWorkflowManager _workflowManager;

        public NotificationsScheduledTaskHandler(IWorkflowManager workflowManager) {
            _workflowManager = workflowManager;
        }

        public void Process(ScheduledTaskContext context) {
            var taskType = context.Task.TaskType;
            if (taskType == Constants.EventUpcomingName ||
                taskType == Constants.EventStartedName ||
                taskType == Constants.EventEndedName ||
                taskType == Constants.EventFollowUpName) {

                var contentItem = context.Task.ContentItem;
                _workflowManager.TriggerEvent(taskType, contentItem,
                    () => new Dictionary<string, object>{
                        { "Content", contentItem }
                    });
            }
        }
    }
}