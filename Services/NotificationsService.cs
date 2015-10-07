using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Orchard.Services;
using Orchard.Tasks.Scheduling;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsService : INotificationsService {
        private readonly IRepository<NotificationsSubscriptionPartRecord> _repository;
        private readonly IScheduledTaskManager _scheduledTaskManager;
        private readonly IContentManager _contentManager;
        private readonly IClock _clock;
        private readonly IAuthorizer _authorizer;

        public NotificationsService(
            IRepository<NotificationsSubscriptionPartRecord> repository, 
            IScheduledTaskManager scheduledTaskManager, 
            IContentManager contentManager, 
            IClock clock, 
            IAuthorizer authorizer) {
            _repository = repository;
            _scheduledTaskManager = scheduledTaskManager;
            _contentManager = contentManager;
            _clock = clock;
            _authorizer = authorizer;
        }

        public void CreateSubscription(NotificationsFormEditViewModel model) {
            // Delete existing
            // TODO: is this needed?
            DeleteSubscriptions(model.EventId, model.UserId);

            // TODO: don't use repository anymore
            _repository.Create(new NotificationsSubscriptionPartRecord {
                EventId = model.EventId,
                UserId = model.UserId,
                SubscribeType = model.SubscribeType,
                TimeDifference = model.TimeDifference,
                SubscribeDifference = model.SubscribeDifference
            });

            var eventDefinition = _contentManager.Get<SchedulingPart>(model.EventId);
            if (eventDefinition.StartDateTime.HasValue) {
                var notifyDate = eventDefinition.StartDateTime.Value;

                switch (model.SubscribeDifference) {
                    case SubscribeDifference.Days:
                        notifyDate = notifyDate.AddDays(model.TimeDifference);
                        break;
                    case SubscribeDifference.Hours:
                        notifyDate = notifyDate.AddHours(model.TimeDifference);
                        break;
                    case SubscribeDifference.Minutes:
                        notifyDate = notifyDate.AddMinutes(model.TimeDifference);
                        break;
                }

                // Add to scheduled tasks
                _scheduledTaskManager.CreateTask(Constants.EventSubscriptionNotification + model.UserId, notifyDate, eventDefinition.ContentItem);
            }
        }

        // TODO: don't use repository anymore, but ContentManager
        public void DeleteSubscription(int id) {
            //var subscription = _repository.Get(s => s.Id == id);
            //if (subscription != null) {
            //    DeleteSubscription(subscription);
            //}
        }

        public void DeleteSubscription(NotificationsSubscriptionPartRecord subscription) {
            _repository.Delete(subscription);
        }

        public void DeleteSubscriptions(int eventId, int userId) {
            //var subscriptions = GetSubscriptions(eventId, userId);
            //foreach (var subscription in subscriptions) {
            //    DeleteSubscription(subscription);
            //}
        }

        public bool CanSubscribeForNotifications(NotificationsPart part) {
            // Not authorized
            if (!_authorizer.Authorize(Permissions.NotificationsPermissions.SubscribeForNotifications, part.ContentItem))
                return false;

            var contentItem = part.ContentItem;
            var schedulingPart = contentItem.As<SchedulingPart>();

            if (schedulingPart == null)
                return false;

            // Cannot subscribe to events in the past when it is not recurring
            if (schedulingPart.StartDateTime < _clock.UtcNow && !schedulingPart.IsRecurring)
                return false;

            return true;
        }

        public IEnumerable<NotificationsSubscriptionPart> GetSubscriptions(int eventId, int userId) {

            var subscriptions = _contentManager
                .Query<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord>()
                .Where(s => s.UserId == userId)
                .Where<CommonPartRecord>(c => c.Container.Id == eventId);

            return subscriptions.List();
        }

        public IEnumerable<NotificationsSubscriptionPart> GetSubscriptions(int eventId) {

            var subscriptions = _contentManager
                .Query<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord>()
                .Where<CommonPartRecord>(c => c.Container.Id == eventId);

            return subscriptions.List();
        } 
    }
}