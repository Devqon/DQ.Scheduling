using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Logging;
using Orchard.Tasks.Scheduling;

namespace DQ.Scheduling.Handlers
{
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventNotifyTaskHandler : IScheduledTaskHandler {
        private readonly IEventService _eventService;
        public EventNotifyTaskHandler(IEventService eventService) {
            _eventService = eventService;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Process(ScheduledTaskContext context) {
            if (context.Task.TaskType != "EventNotify")
                return;

            var eventDefinition = context.Task.ContentItem.As<EventDefinitionPart>();
            if (eventDefinition == null)
                return;

            Logger.Information("Notifying subscribers of event {0}", eventDefinition.Id);

            // TODO: notify
        }
    }
}