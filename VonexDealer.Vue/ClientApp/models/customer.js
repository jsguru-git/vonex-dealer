//import { createAddress } from './Address';
//import { createContact } from './Contact';
//import { createName } from './Name';

export class Customer {
    constructor({
        customerID = 0,
        custNo = null,
        ubAccountNo = 0,
        phoneNumber = null,
        dealerID = 0,
        salutation = null,
        firstName = null,
        surname = null,
        primaryContactName = null,
        position = null,
        contactNo = null,
        phone = null,
        fax = null,
        mobile = null,
        email = null,
        dateAdded = new Date(),
        dob = null,
        planNo = 0,
        billRunCycleNumber = 0,
        contract_start = null,
        contract_end = null,
        contract_Term = null,
        groupNo = null,
        entityType = 0,
        addressID = null,
        companyID = null,
      
} = {}) {
        this.customerID = customerID;
        this.custNo = custNo;
        this.ubAccountNo = ubAccountNo;
        this.phoneNumber = phoneNumber;
        this.dealerID = dealerID;
        this.salutation = salutation;
        this.firstName = firstName;
        this.surname = surname;
        this.primaryContactName = primaryContactName;
        this.position = position;
        this.contactNo = contactNo;
        this.phone = phone;
        this.fax = fax;
        this.mobile = mobile;
        this.email = email;
        this.dateAdded = dateAdded;
        this.dob = dob;
        this.planNo = planNo;
        this.billRunCycleNumber = billRunCycleNumber;
        this.contract_start = contract_start;
        this.contract_end = contract_end;
        this.contract_Term = contract_Term;
        this.groupNo = groupNo;
        this.entityType = entityType;
        this.addressID = addressID;
        this.companyID = companyID;
    }
}

export function createCustomer(data) {
    //const address = createAddress(data.address);
    //const contacts = data.contacts.map(x => createContact(x));
    //const name = createName(data.name);

    return Object.freeze(new Customer(data));
}
