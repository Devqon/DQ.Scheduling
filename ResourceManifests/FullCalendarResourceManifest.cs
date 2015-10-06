using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace DQ.Scheduling.ResourceManifests {
    [OrchardFeature("DQ.SchedulingFullCalendar")]
    public class FullCalendarResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineScript("Moment").SetUrl("ext/fullcalendar/moment.min.js").SetDependencies("jQuery");
            manifest.DefineScript("LangAll").SetUrl("ext/fullcalendar/lang-all.js");
            manifest.DefineScript("FullCalendar").SetUrl("ext/fullcalendar/fullcalendar.min.js", "ext/fullcalendar/fullcalendar.js").SetDependencies("Moment", "jQuery");

            manifest.DefineStyle("FullCalendar").SetUrl("ext/fullcalendar/FullCalendar.css");
            manifest.DefineStyle("FullCalendarPrint").SetUrl("ext/fullcalendar/fullcalendar.print.css").SetDependencies("FullCalendar");
        }
    }
}