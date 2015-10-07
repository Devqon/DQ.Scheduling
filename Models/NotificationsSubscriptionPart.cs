using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPart : ContentPart<NotificationsSubscriptionPartRecord>, ITitleAspect {
        
        // TODO - making a part so we can easily have event handlers off of this (i.e. thanks for subscribing to this event, was it really you)?

        public IContent Event {
            get { return this.As<CommonPart>().Container; }
            set { this.As<CommonPart>().Container = value; }
        }

        // TODO: delete event id, instead use the container property
        public int EventId {
            get { return Retrieve(r => r.EventId); }
            set { Store(r => r.EventId, value); }
        }

        public int? UserId
        {
            get { return Retrieve(r => r.UserId); }
            set { Store(r => r.UserId, value); }
        }

        public string Email
        {
            get { return Retrieve(r => r.Email); }
            set { Store(r => r.Email, value); }
        }

        public string Phone {
            get { return Retrieve(r => r.Phone); }
            set { Store(r => r.Phone, value); }
        }

        public SubscribeType SubscribeType
        {
            get { return Retrieve(r => r.SubscribeType); }
            set { Store(r => r.SubscribeType, value); }
        }

        public string Title {
            get {
                switch (SubscribeType) {
                    default:
                    case SubscribeType.Email:
                        return Email;
                    case SubscribeType.Sms:
                        return Phone;
                    case SubscribeType.Both:
                        return string.Format("{0} / {1}", Email, Phone);
                }
            }
        }
    }
}