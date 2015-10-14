using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Security.Permissions;

namespace DQ.Scheduling.Permissions
{
    public class SchedulingPermissions : IPermissionProvider
    {
        public static readonly Permission ManageScheduledEvents = new Permission { Name = "ManageScheduledEvents" };

        public SchedulingPermissions() {
            T = NullLocalizer.Instance;

            ManageScheduledEvents.Description = T("Manage Scheduled Events").Text;
        }

        public Localizer T { get; set; }
        public Feature Feature { get; private set; }
        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                ManageScheduledEvents
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageScheduledEvents}
                },
                new PermissionStereotype {
                    Name = "Moderator",
                    Permissions = new[] {ManageScheduledEvents}
                }
            };
        }
    }
}