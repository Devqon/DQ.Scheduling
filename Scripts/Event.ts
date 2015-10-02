/// <reference path="ievent.ts" />

module dsc {
    export class Event implements dsc.IEvent {
        constructor(
            public title: string,
            public start: string,
            public end: string,
            public url: string,
            public allDay: boolean) {
        }
    }
}