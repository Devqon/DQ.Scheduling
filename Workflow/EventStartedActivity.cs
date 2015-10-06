using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Workflows.Activities;

namespace DQ.Scheduling.Workflow {
    [OrchardFeature("DQ.SchedulingWorkflows")]
    public class EventStartedActivity : ContentActivity {
        public override string Name {
            get { return Constants.EventStartedName; }
        }

        public override LocalizedString Description {
            get { return T("Event started"); }
        }
    }
}