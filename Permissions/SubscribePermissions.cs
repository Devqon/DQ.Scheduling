using Orchard.Environment.Extensions;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Security.Permissions;
using System.Collections.Generic;

namespace DQ.Scheduling.Permissions {
    [OrchardFeature("DQ.SchedulingDownload")]
    public class SubscribePermissions : IPermissionProvider {
        public static readonly Permission SubscribeToEvent = new Permission { Name = "SubscribeToEvent" };

        public SubscribePermissions() {
            T = NullLocalizer.Instance;

            SubscribeToEvent.Description = T("Subscribe to events").Text;
        }

        public Localizer T { get; set; }
        public Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                SubscribeToEvent
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {SubscribeToEvent}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {SubscribeToEvent}
                },
                new PermissionStereotype {
                    Name = "Moderator",
                    Permissions = new[] {SubscribeToEvent}
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] {SubscribeToEvent}
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] {SubscribeToEvent}
                },
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] {SubscribeToEvent}
                } 
            };
        }
    }
}