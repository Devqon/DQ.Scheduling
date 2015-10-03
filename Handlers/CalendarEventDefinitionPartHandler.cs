using System;
using System.Linq;
using DQ.Scheduling.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.Data;
using Orchard.Fields.Fields;

namespace DQ.Scheduling.Handlers
{
    public class CalendarEventDefinitionPartHandler : ContentHandler
    {
        public CalendarEventDefinitionPartHandler(IRepository<CalendarEventDefinitionRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnUpdated<CalendarEventDefinition>(UpdatePart);
            OnInitializing<CalendarEventDefinition>((ctx, part) => WeldFields(part));
        }

        private void WeldFields(CalendarEventDefinition part) {
            
            // Make sure the fields are always on the part

            part.Weld(new DateTimeField
            {
                PartFieldDefinition = new ContentPartFieldDefinition("StartDateTest")
            });
            part.Weld(new DateTimeField
            {
                PartFieldDefinition = new ContentPartFieldDefinition("EndDateTest")
            });
        }

        private void UpdatePart(UpdateContentContext context, CalendarEventDefinition part) {

            // Update part properties with the field values

            var dateTimeFields = part.Fields.OfType<DateTimeField>().ToList();

            // startdate
            var startDateField = dateTimeFields.FirstOrDefault(f => f.Name == "StartDate");
            if (startDateField != null && startDateField.DateTime != DateTime.MinValue) {
                part.StartDateTime = startDateField.DateTime;
            }

            // enddate
            var endDateField = dateTimeFields.FirstOrDefault(f => f.Name == "EndDate");
            if (endDateField != null && endDateField.DateTime != DateTime.MinValue)
            {
                part.EndDateTime = endDateField.DateTime;
            }
        }
    }
}