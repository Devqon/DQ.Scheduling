namespace DQ.Scheduling {
    public static class Constants {
        // Scheduling Constants
        public static readonly string EventUpcomingName = "EventUpcoming";
        public static readonly string EventStartedName = "EventStarted";
        public static readonly string EventEndedName = "EventEnded";
        public static readonly string EventFollowUpName = "EventFollowUp";
        public static readonly string EventCustomNotification = "EventCustomNotification";

        public static readonly string[] DefaultEventNames = {EventUpcomingName, EventStartedName, EventEndedName, EventFollowUpName};
        
        // Notification Constants
        public static readonly string EventSubscriptionNotification = "EventNotification";
        public static readonly string NotificationsSubscriptionType = "NotificationsSubscription";



        // Email regex, taken from Orchard.Comments/Models/CommentPart
        public const string EmailRegex = @"^(?![\.@])(""([^""\r\\]|\\[""\r\\])*""|([-\w!#$%&'*+/=?^`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
    }
}