using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace DQ.Scheduling.Migrations {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class NotificationsMigrations : DataMigrationImpl {
        public int Create() {

            // NotificationPartRecord table
            SchemaBuilder.CreateTable(typeof(NotificationsPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<bool>("AllowNotifications")
                .Column<int>("NotificationsPlanPartRecord_Id")
            );

            // NotificationsSubscriptionPartRecord table
            SchemaBuilder.CreateTable(typeof(NotificationsSubscriptionPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<int>("UserId") // nullable
                .Column<string>("SubscribeType")
                .Column<string>("Email")
                .Column<string>("Phone")
            );

            // NotificationsPlanPartRecord
            SchemaBuilder.CreateTable(typeof(NotificationsPlanPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<string>("UpcomingNotificationInterval")
                .Column<int>("UpcomingNotificationIntervalCount")
                .Column<string>("FollowUpNotificationInterval")
                .Column<int>("FollowUpNotificationIntervalCount")
            );

            // NotificationsSubscription content type
            ContentDefinitionManager.AlterTypeDefinition(Constants.NotificationsSubscriptionType, type => type
                .WithPart("CommonPart", part => part
                    // Do not show editor for owner
                    .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                .WithPart(typeof(NotificationsSubscriptionPart).Name)
            );

            return 1;
        }
    }
}