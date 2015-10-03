using DQ.Scheduling.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace DQ.Scheduling.Drivers
{
    public class CalendarEventDefinitionDriver : ContentPartDriver<CalendarEventDefinition>
    {
        private const string TemplateName = "Parts/CalendarEventDefinition";

        protected override string Prefix
        {
            get { return "CalendarEventDefinition"; }
        }

        protected override DriverResult Display(CalendarEventDefinition part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_CalendarEventDefinition",
                () => shapeHelper.Parts_CalendarEventDefinition(part));
        }

        protected override DriverResult Editor(CalendarEventDefinition part, dynamic shapeHelper)
        {
            return ContentShape("Parts_CalendarEventDefinition_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CalendarEventDefinition part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}