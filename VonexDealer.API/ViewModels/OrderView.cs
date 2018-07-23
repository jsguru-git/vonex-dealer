using System;
using System.Collections.Generic;
using System.Linq;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;

namespace VonexDealer.API.ViewModels
{
    public class OrderView
    {
        
        public Order order { get; set; }
        public Dealer dealer { get; set; }
        public Customer customer { get; set; }
        public Company company { get; set; }
        public List<Churn> churns { get; set; }
        public List<ISDN> isdn { get; set; }
        public List<NewPSTN> newServices { get; set; }
        public List<Address> Addresses { get; set; }
        public List<RatePlan> ratePlans { get; set; }
        public List<HandsetView> handsets { get; set; }
        public List<Hardware> hardware { get; set; }
        public IPOrder iPOrder { get; set; }
        public List<IPExtension> iPExtensions { get; set; }
        public List<Note> Notes { get; set; }
        public List<Internet> Internets { get; set; }
        public List<Contact> Contacts { get; set; }
        public string FrontEndURL { get; set; }

        public Address getAddress(Int64 addressID)
        {
            Address address = Addresses.Where(x => x.AddressID == addressID).FirstOrDefault();
            return address;
        }
        public string getFormattedAddress(Int64 addressID)
        {
            if (addressID == 0)
                return string.Empty;

            Address address = Addresses.Where(x => x.AddressID == addressID).FirstOrDefault();
            return address.FormattedAddress;
        }

        public string getRatePlanDesc(Int64 rateplanID)
        {
            return ratePlans.Where(x => x.RatePlanID == rateplanID).Select(x => x.RatePlanDescription).FirstOrDefault();
        }

        public Handset getHandset(Int64 handsetID)
        {
            Handset handset = handsets.Select(x => x.Handset).Where(x => x.HandsetID == handsetID).FirstOrDefault();
            if (handset == null)
                return new Handset();
            else
                return handset;
        }

        public string getHandsetDescription(Int64 handsetID)
        {
            Handset handset = handsets.Select(x => x.Handset).Where(x => x.HandsetID == handsetID).FirstOrDefault();
            if (handset == null)
                return string.Empty;
            else
                return $"{handset.Manufacturer.Description} {handset.Model}";
        }

        public Contact getContact(Int64 contactID)
        {
            Contact contact = Contacts.Where(x => x.ContactID == contactID).FirstOrDefault();
            return contact;

        }


    }
}
