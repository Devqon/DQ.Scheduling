/// <reference path="ext/fullcalendar/ifullcalendar.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="event.ts" />

module dsc {

    export class CalendarWidget {
        constructor(id: string, events: dsc.Event[]) {
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
    }
}