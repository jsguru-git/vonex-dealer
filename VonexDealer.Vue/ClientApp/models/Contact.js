export class Contact {
    constructor({ contactID = 0,
        orderID = 0,
        contactName = null,
        contactMobile = null,
        contactEmail = null,
       } = {}) {
        this.contactID = contactID;
        this.orderID = orderID;
        this.contactEmail = contactEmail;
        this.contactMobile = contactMobile;
        this.contactName = contactName;
    }
}

export function createContact(data) {
    return Object.freeze(new Contact(data));
}
