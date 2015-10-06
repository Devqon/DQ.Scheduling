using DQ.Scheduling.Models;
using DQ.Scheduling.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard.Logging;
using Orchard.Messaging.Services;
using Orchard.Security;
using Orchard.Tasks.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventNotifyTaskHandler : Component, IScheduledTaskHandler  {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMessageService _messageService;
        private readonly IShapeFactory _shapeFactory;
        private readonly IShapeDisplay _shapeDisplay;
        private readonly IContentManager _contentManager;

        public EventNotifyTaskHandler(ISubscriptionService subscriptionService, IMessageService messageService, IShapeFactory shapeFactory, IShapeDisplay shapeDisplay, IContentManager contentManager) {
            _subscriptionService = subscriptionService;
            _messageService = messageService;
            _shapeFactory = shapeFactory;
            _shapeDisplay = shapeDisplay;
            _contentManager = contentManager;
        }

        public void Process(ScheduledTaskContext context) {
            // Hacky? user id put into tasktype
            if (!context.Task.TaskType.StartsWith(Constants.EventSubscriptionNotification))
                return;

            var userIdString = context.Task.TaskType.Substring(context.Task.TaskType.IndexOf(Constants.EventSubscriptionNotification, StringComparison.InvariantCulture) + Constants.EventSubscriptionNotification.Length);
            if (string.IsNullOrEmpty(userIdString))
                return;

            int userId;
            // Try getting the user id
            if (!int.TryParse(userIdString, out userId))
                return;

            var eventDefinition = context.Task.ContentItem.As<EventDefinitionPart>();
            if (eventDefinition == null)
                return;

            var subscription = _subscriptionService.GetSubscriptions(eventDefinition.Id, userId).FirstOrDefault();
            if (subscription == null)
                return;

            // Delete subscriptions
            _subscriptionService.DeleteSubscription(subscription);

            var user = _contentManager.Get<IUser>(userId);
            var eventTitle = _contentManager.GetItemMetadata(eventDefinition).DisplayText;

            // TODO:
            // - Use the subscription.SubscribeType: email, sms, ..
            // - Move to (subscription)service
            var template = _shapeFactory.Create("Template_EventNotification", Arguments.From(new {
                EventName = eventTitle
            }));
                
            var parameters = new Dictionary<string, object> {
                        {"Subject", T("Event Notification").Text},
                        {"Body", _shapeDisplay.Display(template)},
                        {"Recipients", user.Email}
                    };

            _messageService.Send("Email", parameters);

            Logger.Information("Notifying user {0} of event {1}", user.UserName, eventTitle);
        }
    }
}