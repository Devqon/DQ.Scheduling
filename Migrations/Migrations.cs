using System;
using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace DQ.Scheduling.Migrations
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            // calendarevent record table
            SchemaBuilder.CreateTable("CalendarEventDefinitionRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("TimeZone")
                    .Column<DateTime>("StartDateTime")
                    .Column<DateTime>("EndDateTime")
                    .Column<bool>("IsAllDay")
                    .Column<bool>("IsRecurring")
            );

            // calendarevent part
            ContentDefinitionManager.AlterPartDefinition(typeof (CalendarEventDefinition).Name,
                part => part
                    .Attachable()
                    .WithDescription("Provides calendar event settings to your content. Use this for displaying events in a Calendar Widget")
            );

            return 1;
        }
    }
}