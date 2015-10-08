using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Migrations {
    [OrchardFeature("DQ.SchedulingCalendar")]
    public class CalendarMigrations : DataMigrationImpl {
        public int Create() {
            // Calendar part record table
            SchemaBuilder.CreateTable(typeof(CalendarPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<int>("QueryId")
                .Column<string>("Plugin")
            );

            // Calendar part
            ContentDefinitionManager.AlterPartDefinition(typeof(CalendarPart).Name, part => part
                .WithDescription("The calendar part")
                .Attachable()
            );

            // Calendar Widget type
            ContentDefinitionManager.AlterTypeDefinition("CalendarWidget", type => type
                .WithPart(typeof(CalendarPart).Name)
                .WithPart("CommonPart")
                .WithPart("WidgetPart")
                .WithSetting("Stereotype", "Widget")
            );

            return 1;
        }
    }
}