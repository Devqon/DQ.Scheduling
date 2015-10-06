using DQ.Scheduling.Models;
using DQ.Scheduling.ViewModels;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Tasks.Scheduling;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    [OrchardFeature("DQ.EventSubscribe")]
    public class SubscriptionService : ISubscriptionService {
        private readonly IRepository<EventSubscriptionRecord> _repository;
        private readonly IScheduledTaskManager _scheduledTaskManager;
        private readonly IContentManager _contentManager;

        public SubscriptionService(IRepository<EventSubscriptionRecord> repository, IScheduledTaskManager scheduledTaskManager, IContentManager contentManager) {
            _repository = repository;
            _scheduledTaskManager = scheduledTaskManager;
            _contentManager = contentManager;
        }

        public void CreateSubscription(EventSubscribeViewModel model) {
            // Delete existing
            // TODO: is this needed?
            DeleteSubscriptions(model.EventId, model.UserId);

            _repository.Create(new EventSubscriptionRecord {
                EventId = model.EventId,
                UserId = model.UserId,
                SubscribeType = model.SubscribeType,
                TimeDifference = model.TimeDifference,
                SubscribeDifference = model.SubscribeDifference
            });

            var eventDefinition = _contentManager.Get<EventDefinitionPart>(model.EventId);
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

        public void DeleteSubscription(int id) {
            var subscription = _repository.Get(s => s.Id == id);
            if (subscription != null) {
                DeleteSubscription(subscription);
            }
        }

        public void DeleteSubscription(EventSubscriptionRecord subscription) {
            _repository.Delete(subscription);
        }

        public void DeleteSubscriptions(int eventId, int userId) {
            var subscriptions = GetSubscriptions(eventId, userId);
            foreach (var subscription in subscriptions) {
                DeleteSubscription(subscription);
            }
        }

        public IEnumerable<EventSubscriptionRecord> GetSubscriptions(int eventId, int userId) {
            return _repository.Fetch(s => s.EventId == eventId && s.UserId == userId);
        } 
    }
}