export class Address {
    constructor({ addressID = 0,
        streetAddress = null,
        suburb = null,
        state = null,
        postCode = null,
        customerID = 0} = {})

    {
        this.addressID = addressID;
        this.streetAddress = streetAddress;
        this.suburb = suburb;
        this.state = state;
        this.postCode = postCode;
        this.customerID = customerID;
    }
}

export function createAddress(data) {
    return Object.freeze(new Address(data));
}
