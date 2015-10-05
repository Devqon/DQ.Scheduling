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
            // eventdefinition record table
            SchemaBuilder.CreateTable(typeof(EventDefinitionPartRecord).Name,
                table => table
                    .ContentPartRecord()

                    .Column<string>("TimeZone")
                    .Column<DateTime>("StartDateTime")
                    .Column<DateTime>("EndDateTime")
                    .Column<bool>("IsAllDay")
                    .Column<bool>("IsRecurring")
            );

            // calendarevent part
            ContentDefinitionManager.AlterPartDefinition(typeof(EventDefinitionPart).Name,
                part => part
                    .Attachable()
                    .WithDescription("Provides event settings to your content.")
            );

            return 1;
        }
    }
}