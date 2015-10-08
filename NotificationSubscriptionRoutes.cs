using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace DQ.Scheduling
{
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationSubscriptionRoutes : IRouteProvider {
        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "Notifications/Subscriptions",
                        new RouteValueDictionary {
                            {"area", "DQ.Scheduling"},
                            {"controller", "Notifications"},
                            {"action", "Subscriptions"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "DQ.Scheduling"}
                        },
                        new MvcRouteHandler())
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }
    }
}