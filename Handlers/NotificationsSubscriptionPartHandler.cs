using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System.Linq;
using System;

namespace DQ.Scheduling.Handlers {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPartHandler : ContentHandler  {
        
        public NotificationsSubscriptionPartHandler(IRepository<NotificationsSubscriptionPartRecord> repository, IContentDefinitionManager contentDefinitionManager) {
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<NotificationsSubscriptionPart>(TriggerNotificationsSubscriptionUpdatedEvents);
            OnUnpublished<NotificationsSubscriptionPart>(TriggerNotificationsSubscriptionUnpublishedEvents);
        }
        
        private void TriggerNotificationsSubscriptionUpdatedEvents(UpdateContentContext context, NotificationsSubscriptionPart part) {
            // TODO: these triggers / iterating over interfaces are what will allow other modules to integrate
        }

        private void TriggerNotificationsSubscriptionUnpublishedEvents(PublishContentContext context, NotificationsSubscriptionPart part) {
            // TODO: these triggers / iterating over interfaces are what will allow other modules to integrate
        }


    }
}