export class Landline {
    constructor({
        landlineID = 0,
        orderID = 0,
        churns = [],
        newServices = [],
        isdNs = []
    } = {}) {
        this.landlineID = landlineID;
        this.orderID = orderID;
        this.churns = churns;
        this.newServices = newServices;
        this.isdNs = isdNs;
    }

}
export function createLandline(data) {
    return Object.freeze(new Landline(data));
}
