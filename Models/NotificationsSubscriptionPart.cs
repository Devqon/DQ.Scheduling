using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPart : ContentPart<NotificationsSubscriptionPartRecord> {
        
        // TODO - making a part so we can easily have event handlers off of this (i.e. thanks for subscribing to this event, was it really you)?

    }
}