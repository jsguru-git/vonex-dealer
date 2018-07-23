export class Summary {
    constructor({
        to = '',
        subject = '',
        emailBody = '',
        orderID = 0,

    } = {}) {
        this.to = to;
        this.subject = subject;
        this.emailBody = emailBody;
        this.orderID = orderID;


    }
}

export function CreateSummary(data) {
    return Object.freeze(new Summary(data))
}

