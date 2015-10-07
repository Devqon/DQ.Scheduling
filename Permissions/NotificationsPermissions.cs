using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Security.Permissions;

namespace DQ.Scheduling.Permissions
{
    public class NotificationsPermissions : IPermissionProvider
    {
        public static readonly Permission SubscribeForNotifications = new Permission { Name = "SubscribeForNotifications" };

        public NotificationsPermissions() {
            T = NullLocalizer.Instance;

            SubscribeForNotifications.Description = T("Subscribe for notifications.").Text;
        }

        public Localizer T { get; set; }
        public Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                SubscribeForNotifications
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Moderator",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] {SubscribeForNotifications}
                },
                new PermissionStereotype {
                    Name = "Anonymous",
                    Permissions = new[] {SubscribeForNotifications}
                }
            };
        }
    }
}