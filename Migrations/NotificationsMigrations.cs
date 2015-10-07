using DQ.Scheduling.Models;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Migrations {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsMigrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable(typeof(NotificationsPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<bool>("AllowNotifications"));

            SchemaBuilder.CreateTable(typeof(NotificationsSubscriptionPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<int>("EventId", column => column.NotNull())
                .Column<int>("UserId", column => column.NotNull())
                .Column<string>("SubscribeType")
                .Column<int>("TimeDifference")
                .Column<string>("SubscribeDifference"));

            return 1;
        }
    }
}