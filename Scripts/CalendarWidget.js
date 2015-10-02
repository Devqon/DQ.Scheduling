/// <reference path="ext/fullcalendar/ifullcalendar.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="event.ts" />
var dsc;
(function (dsc) {
    var CalendarWidget = (function () {
        function CalendarWidget(id, events) {
            jQuery('#' + id).fullCalendar({
                lang: navigator.userLanguage,
                theme: false,
                header: {
                    left: 'prev,next prevYear,nextYear today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                editable: false,
                timezone: 'local',
                events: events,
                weekNumbers: false,
                fixedWeekCount: false
            });
        }
        return CalendarWidget;
    })();
    dsc.CalendarWidget = CalendarWidget;
})(dsc || (dsc = {}));
//# sourceMappingURL=CalendarWidget.js.map