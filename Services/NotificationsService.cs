using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Orchard.Services;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsService : INotificationsService {
        private readonly IContentManager _contentManager;
        private readonly IClock _clock;
        private readonly IAuthorizer _authorizer;

        public NotificationsService(
            IContentManager contentManager, 
            IClock clock, 
            IAuthorizer authorizer) {
            _contentManager = contentManager;
            _clock = clock;
            _authorizer = authorizer;
        }

        public IContentQuery<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord> GetNotificationsSubscriptionQuery() {
            return _contentManager
                .Query<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord>();
        } 

        public void DeleteSubscription(int id) {
            var subscription = GetSubscription(id);
            DeleteSubscription(subscription);
        }

        public void DeleteSubscription(NotificationsSubscriptionPart subscription) {
            _contentManager.Remove(subscription.ContentItem);
        }

        public void DeleteSubscriptions(int eventId, int userId) {
            var subscriptions = GetSubscriptionsForEventAndUser(eventId, userId);
            foreach (var subscription in subscriptions) {
                DeleteSubscription(subscription);
            }
        }

        public void DeleteSubscriptions(int eventId, string email) {
            var subscriptions = GetNotificationsSubscriptionQuery()
                .Where(s => s.Email == email)
                .List();

            foreach (var subscription in subscriptions) {
                DeleteSubscription(subscription);
            }
        }

        public bool CanSubscribeForNotifications(NotificationsPart part) {

            // Not enabled for this content item
            if (!part.AllowNotifications)
                return false;

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

        public NotificationsSubscriptionPart GetSubscription(int id) {
            return _contentManager.Get<NotificationsSubscriptionPart>(id);
        }

        public IEnumerable<NotificationsSubscriptionPart> GetSubscriptionsForEventAndUser(int eventId, int userId) {

            // TODO: can only be one?
            var subscriptions = GetNotificationsSubscriptionQuery()
                .Where(s => s.UserId == userId)
                .Where<CommonPartRecord>(c => c.Container.Id == eventId);

            return subscriptions.List();
        }

        public IEnumerable<NotificationsSubscriptionPart> GetSubscriptionsForEvent(int eventId) {

            var subscriptions = GetNotificationsSubscriptionQuery()
                .Where<CommonPartRecord>(c => c.Container.Id == eventId);

            return subscriptions.List();
        }
    }
}