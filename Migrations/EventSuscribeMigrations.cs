using DQ.Scheduling.Models;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Migrations
{
    [OrchardFeature("DQ.EventSubscribe")]
    public class EventSuscribeMigrations : DataMigrationImpl
    {
        public int Create() {

            SchemaBuilder.CreateTable(typeof (EventSubscribePartRecord).Name,
                table => table

                    .ContentPartRecord()
                    .Column<bool>("AllowSubscribe"));

            SchemaBuilder.CreateTable(typeof (EventSubscriptionRecord).Name,
                table => table

                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("EventId", column => column.NotNull())
                    .Column<int>("UserId", column => column.NotNull())
                    .Column<string>("SubscribeType")
                    .Column<int>("TimeDifference")
                    .Column<string>("SubscribeDifference"));

            return 1;
        }

        public int UpdateFrom1() {

            SchemaBuilder.AlterTable(typeof (EventSubscribePartRecord).Name,
                table => table

                    .DropColumn("AllowSubscribe"));

            SchemaBuilder.AlterTable(typeof (EventSubscribePartRecord).Name,
                table => table

                    .AddColumn<bool>("AllowSubscriptions"));

            return 2;
        }
    }
}