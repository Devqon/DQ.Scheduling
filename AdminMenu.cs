using Orchard.Localization;
using Orchard.UI.Navigation;

namespace DQ.Scheduling {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("scheduledEvents")
                .Add(T("Scheduled Events"), "4",
                    menu => menu.Add(T("List"), "0", item => item.Action("Index", "SchedulingAdmin", new { area = "DQ.Scheduling" })));
        }
    }
}
