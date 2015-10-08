using DQ.Scheduling.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
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
                // TODO: maybe make sure the part is always there by utilizing 
                // Filters.Add(new ActivatingFilter<NotificationsSubscriptionPart>(Constants.NotificationsSubscriptionType))
                .WithPart(typeof(NotificationsSubscriptionPart).Name)
            );

            return 1;
        }

        public int UpdateFrom1() {

            // NotificationsPlanPartRecord
            SchemaBuilder.CreateTable(typeof(NotificationsPlanPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<string>("UpcomingNotificationInterval")
                .Column<int>("UpcomingNotificationIntervalCount")
                .Column<string>("FollowUpNotificationInterval")
                .Column<int>("FollowUpNotificationIntervalCount")
            );

            return 2;
        }

        public int UpdateFrom2() {
            
            ContentDefinitionManager.AlterPartDefinition(typeof(NotificationsPlanPart).Name, part => part
                .Attachable());

            return 3;
        }
    }
}