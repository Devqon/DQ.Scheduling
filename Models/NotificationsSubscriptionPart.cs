using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Models {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsSubscriptionPart : ContentPart<NotificationsSubscriptionPartRecord>, ITitleAspect {

        [Required]
        public IContent Event {
            get { return this.As<ICommonPart>().Container; }
            set { this.As<ICommonPart>().Container = value; }
        }

        public int? UserId {
            get { return Retrieve(r => r.UserId); }
            set { Store(r => r.UserId, value); }
        }

        [RegularExpression(Constants.EmailRegex)]
        public string Email {
            get { return Retrieve(r => r.Email); }
            set { Store(r => r.Email, value); }
        }

        public string Phone {
            get { return Retrieve(r => r.Phone); }
            set { Store(r => r.Phone, value); }
        }

        public SubscribeType SubscribeType {
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