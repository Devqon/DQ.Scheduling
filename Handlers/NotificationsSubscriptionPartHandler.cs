using System.Web.Routing;
using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Orchard.Environment.Extensions;

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

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            var notificationsSubscription = context.ContentItem.As<NotificationsSubscriptionPart>();

            if (notificationsSubscription == null)
                return;

            context.Metadata.CreateRouteValues = new RouteValueDictionary {
                {"Area", "DQ.Scheduling"},
                {"Controller", "Notifications"},
                {"Action", "Subscribe"}
            };

            context.Metadata.RemoveRouteValues = new RouteValueDictionary {
                {"Area", "DQ.Scheduling"},
                {"Controller", "Notifications"},
                {"Action", "UnSubscribe"}
            };
        }
    }
}