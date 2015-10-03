using System;
using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace DQ.Scheduling
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
                    .Column<string>("Url")
                    .Column<bool>("IsRecurring")
            );

            // calendarevent part
            ContentDefinitionManager.AlterPartDefinition(typeof (CalendarEventDefinition).Name,
                part => part
                    .Attachable()
                    .WithDescription("Provides calendar event settings to your content. Use this for displaying events in a Calendar Widget")
                    .WithField("StartDate", field => field
                        // defaults to date AND time
                        .OfType("DateTimeField"))
                    .WithField("EndDate", field => field
                        .OfType("DateTimeField"))
            );

            // calendarwidget record table
            SchemaBuilder.CreateTable("CalendarWidgetPartRecord", table => table
                .ContentPartRecord()
                .Column<int>("QueryId")
            );

            // calendarwidget part
            ContentDefinitionManager.AlterPartDefinition(typeof (CalendarWidgetPart).Name,
                part => part
                    .Attachable()
                    .WithDescription("The calendar widget part")
            );

            // calendarwidget type
            ContentDefinitionManager.AlterTypeDefinition("CalendarWidget",
                type => type
                    .WithPart("CalendarWidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
            );

            return 1;
        }
    }
}