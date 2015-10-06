using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    public interface ISubscriptionService : IDependency {
        /// <summary>
        /// Create a subscription to an event
        /// </summary>
        /// <param name="model"></param>
        void CreateSubscription(EventSubscribeViewModel model);

        void DeleteSubscriptions(int eventId, int userId);
        void DeleteSubscription(int id);
        IEnumerable<EventSubscriptionRecord> GetSubscriptions(int eventId, int userId);
        void DeleteSubscription(EventSubscriptionRecord subscription);
    }
}