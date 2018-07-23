export class VonexMobileService {
    constructor({
        NumberOfService = null,
        isNewService = false,
        extensionData = []
    } = {}) {
        this.NumberOfService = NumberOfService;
        this.isNewService = isNewService;
        this.extensionData = extensionData;
    }
}

export function CreateNewVonexMobileService(data) {
    return Object.freeze(new VonexMobileService(data))
}
