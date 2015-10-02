using Orchard.UI.Resources;

namespace dsc.CalendarWidget
{
    public class ResourceManifest:IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("Moment").SetUrl("ext/fullcalendar/moment.min.js").SetDependencies("jQuery");
            manifest.DefineScript("LangAll").SetUrl("ext/fullcalendar/lang-all.js");
            manifest.DefineScript("FullCalendar").SetUrl("ext/fullcalendar/fullcalendar.min.js", "ext/fullcalendar/fullcalendar.js").SetDependencies("Moment", "jQuery");
            manifest.DefineScript("CalendarWidget").SetUrl("CalendarWidget.js").SetDependencies("FullCalendar");

            manifest.DefineStyle("FullCalendar").SetUrl("ext/fullcalendar/FullCalendar.css");
            manifest.DefineStyle("FullCalendarPrint").SetUrl("ext/fullcalendar/fullcalendar.print.css").SetDependencies("FullCalendar");
            manifest.DefineStyle("CalendarWidget").SetUrl("CalendarWidget.css").SetDependencies("FullCalendar");
        }
    }
}