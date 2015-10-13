using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using System;

namespace DQ.Scheduling.Migrations {
    [OrchardFeature("DQ.Scheduling")]
    public class SchedulingMigrations : DataMigrationImpl  {
        public int Create() {
            // Event Definition record table
            SchemaBuilder.CreateTable(typeof(SchedulingPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<string>("TimeZone")
                .Column<DateTime>("StartDateTime")
                .Column<DateTime>("EndDateTime")
                .Column<bool>("IsAllDay")
                .Column<bool>("IsRecurring")
            );

            // Calendar Event part
            ContentDefinitionManager.AlterPartDefinition(typeof(SchedulingPart).Name, part => part
                .WithDescription("Provides event settings to your content.")
                .Attachable()
            );

            return 1;
        }

        public int UpdateFrom1() {

            SchemaBuilder.AlterTable(typeof(SchedulingPartRecord).Name, table => table
                .DropColumn("TimeZone"));

            SchemaBuilder.AlterTable(typeof(SchedulingPartRecord).Name, table => table
                .AddColumn<string>("DisplayUrlOverride"));

            return 2;
        }
    }
}