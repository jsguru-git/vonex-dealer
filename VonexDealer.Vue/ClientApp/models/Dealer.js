export class Dealer {
    constructor({ dealerID = null,
        dealerFullName = null,
        dealerEmail = null,
        uBDealerID = null } = {}) {
        this.dealerID = dealerID;
        this.dealerFullName = dealerFullName;
        this.dealerEmail = dealerEmail;
        this.uBDealerID = uBDealerID;
    }
}

export function createDealer(data) {
    return Object.freeze(new Dealer(data));
}
