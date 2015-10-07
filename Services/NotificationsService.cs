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

        public void DeleteSubscription(int id) {
            var subscription = GetSubscription(id);
            DeleteSubscription(subscription);
        }

        public void DeleteSubscription(NotificationsSubscriptionPart subscription) {
            _contentManager.Remove(subscription.ContentItem);
        }

        public void DeleteSubscriptions(int eventId, int userId) {
            var subscriptions = GetSubscriptions(eventId, userId);
            foreach (var subscription in subscriptions) {
                DeleteSubscription(subscription);
            }
        }

        public void DeleteSubscriptions(int eventId, string email) {
            var subscriptions = _contentManager
                .Query<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord>()
                .Where(s => s.Email == email)
                .List();

            foreach (var subscription in subscriptions) {
                DeleteSubscription(subscription);
            }
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

        public NotificationsSubscriptionPart GetSubscription(int id) {
            return _contentManager.Get<NotificationsSubscriptionPart>(id);
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