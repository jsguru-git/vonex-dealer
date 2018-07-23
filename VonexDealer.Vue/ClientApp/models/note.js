export class Note {
    constructor({ noteID = 0,
        orderID = 0,
        component = null,
        noteText = null } = {}) {
        this.noteID = noteID;
        this.orderID = orderID;
        this.component = component;
        this.noteText = noteText;
    }
}

export function createNote(data) {
    return Object.freeze(new Note(data));
}
