using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace DQ.Scheduling.ResourceManifests
{
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineScript("NotificationsSubscribe_List").SetUrl("admin-subscriptionlist.js").SetDependencies("jQuery");
        }
    }
}