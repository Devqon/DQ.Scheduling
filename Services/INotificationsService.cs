using DQ.Scheduling.Models;
using Orchard;
using System.Collections.Generic;
using Orchard.ContentManagement;

namespace DQ.Scheduling.Services {
    public interface INotificationsService : IDependency {

        IEnumerable<NotificationsSubscriptionPart> GetSubscriptionsForEventAndUser(int eventId, int userId);
        bool CanSubscribeForNotifications(NotificationsPart part);
        IEnumerable<NotificationsSubscriptionPart> GetSubscriptionsForEvent(int eventId);
        void DeleteSubscription(int id);
        void DeleteSubscription(NotificationsSubscriptionPart subscription);
        void DeleteSubscriptions(int eventId, int userId);
        void DeleteSubscriptions(int eventId, string email);
        NotificationsSubscriptionPart GetSubscription(int id);
        IContentQuery<NotificationsSubscriptionPart, NotificationsSubscriptionPartRecord> GetNotificationsSubscriptionQuery();
        void UpdateScheduleTasks(NotificationsPart notification);
        void DeleteExistingScheduleTasks(ContentItem contentItem);
    }
}