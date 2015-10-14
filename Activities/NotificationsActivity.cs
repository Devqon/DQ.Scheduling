using System.Collections.Generic;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Workflows.Models;
using Orchard.Workflows.Services;

namespace DQ.Scheduling.Activities {
    [OrchardFeature("DQ.SchedulingNotifications")]
    public abstract class NotificationsActivity: Event {
        public Localizer T { get; set; }

        public override bool CanStartWorkflow {
            get { return true; }
        }

        public override bool CanExecute(WorkflowContext workflowContext, ActivityContext activityContext) {
            return true;
        }

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext) {
            return new[] { T("Done") };
        }

        public override IEnumerable<LocalizedString> Execute(WorkflowContext workflowContext, ActivityContext activityContext) {
            yield return T("Done");
        }

        public override LocalizedString Category {
            get { return T("Scheduling"); }
        }
    }

    #region Notifications from the Task Scheduler
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventUpcomingActivity : NotificationsActivity {
        public override string Name {
            get { return Constants.EventUpcomingName; }
        }

        public override LocalizedString Description {
            get { return T("Event Upcoming"); }
        }
    }

    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventStartedActivity : NotificationsActivity {
        public override string Name {
            get { return Constants.EventStartedName; }
        }

        public override LocalizedString Description {
            get { return T("Event Started"); }
        }
    }

    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventEndedActivity : NotificationsActivity {
        public override string Name {
            get { return Constants.EventEndedName; }
        }

        public override LocalizedString Description {
            get { return T("Event Ended"); }
        }
    }

    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventFollowUpActivity : NotificationsActivity {
        public override string Name {
            get { return Constants.EventFollowUpName; }
        }

        public override LocalizedString Description {
            get { return T("Event Follow Up"); }
        }
    }
    #endregion

    #region Notifications outside of the Task Scheduler
    [OrchardFeature("DQ.SchedulingNotifications")]
    public class EventCustomNotification : NotificationsActivity {
        public override string Name {
            get { return Constants.EventCustomNotification; }
        }

        public override LocalizedString Description {
            get { return T("Event Custom Notification"); }
        }
    }
    #endregion
}