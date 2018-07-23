export class newService {
    constructor({
        NumberOfService = null,
        isNewService = false,
        serviceData = [{
            id: 0,
            Address: 0,
            Terminate: 0,
            Fee: 0
        }]
    } = {}) {
        this.NumberOfService = NumberOfService;
        this.isNewService = isNewService;
        this.serviceData = serviceData;
    }
}

export function CreatenewService(data) {
    return Object.freeze(new newService(data))
}
