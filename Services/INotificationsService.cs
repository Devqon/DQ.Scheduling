using DQ.Scheduling.Models;
using Orchard;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    public interface INotificationsService : IDependency {

        IEnumerable<NotificationsSubscriptionPart> GetSubscriptions(int eventId, int userId);
        bool CanSubscribeForNotifications(NotificationsPart part);
        IEnumerable<NotificationsSubscriptionPart> GetSubscriptions(int eventId);
        void DeleteSubscription(int id);
        void DeleteSubscription(NotificationsSubscriptionPart subscription);
        void DeleteSubscriptions(int eventId, int userId);
        void DeleteSubscriptions(int eventId, string email);
        NotificationsSubscriptionPart GetSubscription(int id);
    }
}