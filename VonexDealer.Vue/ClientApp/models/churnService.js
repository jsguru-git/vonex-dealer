export class churnService {
    constructor({
        id = 0,
        ContractPeriod= 0,
        ServiceNumber= null,
        RatePlan= 0,
        ServiceType= 0,
        Address= null

} = {}) {
        this.id = id;
        this.ContractPeriod = ContractPeriod;
        this.ServiceNumber = ServiceNumber;
        this.RatePlan = RatePlan;
        this.ServiceType = ServiceType;
        this.Address = Address;
    }
}

export function createChurnService(data) {
    //const address = createAddress(data.address);
    //const contacts = data.contacts.map(x => createContact(x));
    //const name = createName(data.name);

    return Object.freeze(new churnService(data));
}
