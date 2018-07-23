export class NewServices {
    constructor({
        NumberOfService = null,
        isNewService = false,
        serviceData = []
    } = {}) {
        this.NumberOfService = NumberOfService;
        this.isNewService = isNewService;
        this.serviceData = serviceData;
    }
}

export function CreateNewServices(data) {
    return Object.freeze(new NewServices(data))
}
