using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    public interface INotificationsService : IDependency {
        /// <summary>
        /// Create a subscription to an event
        /// </summary>
        /// <param name="model"></param>
        void CreateSubscription(NotificationsFormEditViewModel model);

        void DeleteSubscriptions(int eventId, int userId);
        void DeleteSubscription(int id);
        IEnumerable<NotificationsSubscriptionPartRecord> GetSubscriptions(int eventId, int userId);
        void DeleteSubscription(NotificationsSubscriptionPartRecord subscription);
        bool CanSubscribeForNotifications(NotificationsPart part);
    }
}