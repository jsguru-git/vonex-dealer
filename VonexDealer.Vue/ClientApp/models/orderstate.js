//import { createAddress } from './Address';
//import { createContact } from './Contact';

export class OrderState {
    constructor(
        { companyID = 0, orderID = 0, dealerID = 0, customerID = 0 }
            = {}) {
        this.companyID = companyID;
        this.orderID = orderID;
        this.dealerID = dealerID;
        this.customerID = customerID;
    }
}

export function createOrderState(data) {
    //const address = createAddress(data.address);
    //const contact = createContact(data.contact);
    return Object.freeze(new OrderState(data));
    //return Object.freeze(new OrderState({
    //    companyID: data.companyID,
    //    orderID: data.orderID,
    //    dealerID: data.dealerID,
    //    customerID: data.customerID
    //}));
}
