using System.Collections.Generic;
using System.Linq;
using DQ.Scheduling.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Projections.Models;
using Orchard.Projections.Services;

namespace DQ.Scheduling.Services {
    public class EventService : IEventService {

        private readonly IOrchardServices _orchardServices;
        private readonly IProjectionManager _projectionManager;

        public EventService(IOrchardServices orchardServices, IProjectionManager projectionManager) {
            _orchardServices = orchardServices;
            _projectionManager = projectionManager;
        }

        public List<QueryPart> GetEventDefinitionQueries()
        {
            var queryParts = _orchardServices.ContentManager.Query<QueryPart, QueryPartRecord>("Query").List();

            var eventDefinitionQueries = new List<QueryPart>();

            foreach (QueryPart part in queryParts)
            {
                var contentItem = _projectionManager.GetContentItems(part.Id).FirstOrDefault();
                if (contentItem == null)
                {
                    return new List<QueryPart>();
                }

                // Check if has eventdefinition part
                if (contentItem.TypeDefinition.Parts.Any(p => p.PartDefinition.Name == typeof (EventDefinitionPart).Name))
                {
                    eventDefinitionQueries.Add(part);
                }
            }

            return eventDefinitionQueries;
        }

        public void SubscribeToEvent(EventDefinitionPart eventDefinitionPart, SubscribeType subscribeType) {

            var user = _orchardServices.WorkContext.CurrentUser;
            // TODO: implementation:
            // - Check subscribe type
            // - Save subscribe type
            // - Add to notify task handler

            switch (subscribeType) {
                case SubscribeType.Email:
                    var email = user.Email;
                    // Save notify options
                    break;
                case SubscribeType.Sms:
                    // ..
                    break;
            }
        }

        public void NotifyEventSubscribers(EventDefinitionPart eventDefinitionPart) {
            // TODO

            // Get subscribers

            // Notify subscribers
            // - Email?
            // - INotifier?
            // - Workflow?
        }

    }
}