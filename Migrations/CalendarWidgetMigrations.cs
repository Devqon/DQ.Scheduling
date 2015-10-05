﻿using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Migrations
{
    [OrchardFeature("DQ.CalendarWidget")]
    public class CalendarWidgetMigrations : DataMigrationImpl
    {
        public int Create()
        {

            // calendarwidget record table
            SchemaBuilder.CreateTable(typeof(CalendarWidgetPartRecord).Name,
                table => table
                    .ContentPartRecord()

                    .Column<int>("QueryId")
            );

            // calendarwidget part
            ContentDefinitionManager.AlterPartDefinition(typeof(CalendarWidgetPart).Name,
                part => part
                    .Attachable()

                    .WithDescription("The calendar widget part")
            );

            // calendarwidget type
            ContentDefinitionManager.AlterTypeDefinition("CalendarWidget",
                type => type
                    .WithPart(typeof(CalendarWidgetPart).Name)
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
            );

            return 1;
        }

        public int UpdateFrom1() {

            SchemaBuilder.AlterTable(typeof (CalendarWidgetPartRecord).Name,
                table => table
                    .AddColumn<string>("Plugin"));

            return 2;
        }
    }
}