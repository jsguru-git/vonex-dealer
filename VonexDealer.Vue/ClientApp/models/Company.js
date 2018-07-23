export class Company {
    constructor({ companyID = 0,
        companyName = null,
        tradingName = null,
        entityType = null,
        directorSurname = null,
        abn = null,
        acn = null,
        driversLicenseNumber = null,
        dateOfBirth = new Date("1901-01-01"),
        directorGivenName = null,
        otherOrganisation = false,
        guaranteeAttached = false,
        dobConsumer = new Date("1901-01-01"),
        isdirectorSurnameRequired = false,
        isdateOfBirthRequired = false
        } = {}) {
        this.companyID = companyID;
        this.companyName = companyName;
        this.tradingName = tradingName;
        this.entityType = entityType;
        this.directorSurname = directorSurname;
        this.abn = abn;
        this.acn = acn;
        this.driversLicenseNumber = driversLicenseNumber;
        this.dateOfBirth = dateOfBirth;
        this.directorGivenName = directorGivenName;
        this.otherOrganisation = otherOrganisation;
        this.guaranteeAttached = guaranteeAttached;
        this.dobConsumer = dobConsumer;
        this.isdirectorSurnameRequired = isdirectorSurnameRequired;
        this.isdateOfBirthRequired = isdateOfBirthRequired;
    }
}

export function createCompany(data) {
    return Object.freeze(new Company(data));
}
