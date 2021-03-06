﻿using DQ.Scheduling.CalendarProviders;
using DQ.Scheduling.Models;
using Orchard;
using System.Collections.Generic;

namespace DQ.Scheduling.Services {
    public interface ICalendarService : IDependency {
        IEnumerable<FormattedEvent> GetFormattedCalendarEvents(CalendarPart part);
        IList<string> GetCalendarPlugins();
    }
}