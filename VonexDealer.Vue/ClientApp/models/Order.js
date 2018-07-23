export class Order {
    constructor({
        orderID = 0
        , orderStatus = 0
        , createdDate = new Date()
        , customerID = 0
        , dealerID = 0
        , directDebitID = 0
        , hardwareID = 0
        , iPOrderID = 0
        , internetID = 0
        , landlineID = 0
        , signature = ''
        , orderGUID = '{00000000-0000-0000-0000-000000000000}'
    } = {}) {
        this.orderID = orderID;
        this.orderStatus = orderStatus;
        this.createdDate = createdDate;
        this.customerID = customerID;
        this.dealerID = dealerID;
        this.directDebitID = directDebitID;
        this.hardwareID = hardwareID;
        this.iPOrderID = iPOrderID;
        this.internetID = internetID;
        this.landlineID = landlineID;
        this.signature = signature;
        this.orderGUID = orderGUID;
    }
}

export function createOrder(data) {
    return Object.freeze(new Order(data));
}
