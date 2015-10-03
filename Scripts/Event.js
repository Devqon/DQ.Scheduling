/// <reference path="ievent.ts" />
var dsc;
(function (dsc) {
    var Event = (function () {
        function Event(title, start, end, url, allDay) {
            this.title = title;
            this.start = start;
            this.end = end;
            this.url = url;
            this.allDay = allDay;
        }
        return Event;
    })();
    dsc.Event = Event;
})(dsc || (dsc = {}));
//# sourceMappingURL=event.js.map