using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
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

        public int UpdateFrom1() {

            SchemaBuilder.AlterTable(typeof(NotificationsSubscriptionPartRecord).Name, table => table
                .AddColumn<string>("Email"));

            SchemaBuilder.AlterTable(typeof(NotificationsSubscriptionPartRecord).Name, table => table
                .AddColumn<string>("Phone"));

            return 2;
        }

        public int UpdateFrom2() {

            ContentDefinitionManager.AlterTypeDefinition("NotificationSubscription", type => type
                .WithPart("CommonPart", part => part
                    // Do not show editor for owner
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                .WithPart(typeof(NotificationsSubscriptionPart).Name));

            return 3;
        }
    }
}