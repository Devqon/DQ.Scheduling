using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Security.Permissions;

namespace DQ.Scheduling
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission SubscribeToEvent = new Permission { Name = "SubscribeToEvent" };

        public Permissions() {
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