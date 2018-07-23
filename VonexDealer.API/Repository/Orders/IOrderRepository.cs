using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.Models.UB;
using VonexDealer.API.ViewModels;

namespace VonexDealer.API.Repository.Orders
{
    /// <summary>
    /// 
    /// The following actions are to comprise the entire functionality of the interface to orders
    /// All the edits made to an order are to be asyncronous and each module is to be separate, but
    /// also able to be combined as a complete order.
    /// 
    /// create order
    /// add/update/delete
    /// customer to order
    /// company to order
    /// landline to order
    /// IPOrder to order
    /// internet to order
    /// mobile to order
    /// inbound to order
    /// PAF to order
    /// notes to order
    /// 
    /// </summary>
    /// 
    public interface IOrderRepository
    {
        String GetApplicationUrl();
        Task<List<Address>> GetAddressesOnOrderAsync(Int64 orderID);
        Int64 loggedInDealerID(ClaimsPrincipal dealer);
        Task<Dealer> getDealerAsync(Int64 dealerID);
        Task<Dealer> updateDealer(Int64 dealerID, Dealer dealer);
        Task<Dealer> addDealer(Dealer dealer);
        Task<bool> removeDealer(Int64 dealerID);
        IQueryable<Dealer> getDealers();
        Task<bool> UpdateSignature(Guid orderID, string signature);
        Task<bool> UpdateOrderStatus(Int64 orderID, int status);


        Task<List<HandsetView>> getHandsetsAsync(Int64 categoryID = 0, Int64 manufacturerID = 0);
        Task<List<HardwareCategory>> getHardwareCategoriesAsync();
        Task<List<Manufacturer>> getManufacturersAsync();
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Int64 orderID, Order order);
        Task<Order> CancelOrderAsync(Int64 orderID);
       
        IQueryable<Order> GetOrders(Int64 orderID = 0, string dealerEmail = "");
        IQueryable<Company> GetCompanys();
        IQueryable<RatePlan> GetRatePlans(Int64 IpOrderID = 0);

        Task<Customer> AddCustomerToOrderAsync(Int64 orderID, Customer customer );
        Task<Customer> UpdateCustomerOnOrderAsync(Int64 orderID, Customer customer);
        Task<bool> RemoveCustomerFromOrderAsync(Int64 orderID, Int64 customerID);

        Task<Company> AddCompanyToOrderAsync(Int64 orderID, Company Company);
        Task<Company> UpdateCompanyOnOrderAsync(Int64 orderID, Company Company);
        Task<bool> RemoveCompanyFromOrderAsync(Int64 orderID, Int64 CompanyID);

        Task<Company> AddCompanyAsync( Company Company);
        Task<Company> UpdateCompanyAsync(Company Company);
        Task<bool> RemoveCompanyAsync(Int64 companyID);
        Task<Company> GetCompanyAsync(Int64 companyID);


        Task<Landline> AddLandlineToOrderAsync(Int64 orderID, Landline Landline);
        Task<Landline> UpdateLandlineOnOrderAsync(Int64 orderID, Landline Landline);
        Task<bool> RemoveLandlineFromOrderAsync(Int64 orderID, Int64 LandlineID);
        Task<Churn[]> RemovePSTNChurnFromOrderAsync(long orderID, Churn[] churns);
        Task<NewPSTN[]> RemoveNewPSTNFromOrderAsync(long orderID, NewPSTN[] pstns);

        //IPOrder
        Task<IPOrder> AddIPOrderToOrderAsync(Int64 orderID, IPOrder IPOrder);
        Task<List<IPExtension>> AddIPExtensionsToOrderAsync(Int64 orderID, List<IPExtension> extensions);
        Task<IPExtension[]> RemoveIPExtensionsAsync(Int64 orderID, IPExtension[] extensions);
        Task<IPOrder> UpdateIPOrderOnOrderAsync(Int64 orderID, IPOrder IPOrder);
        Task<bool> RemoveIPOrderFromOrderAsync(Int64 orderID, Int64 IPOrderID);
        Task<IPOrder> GetIpOnOrderAsync(Int64 orderID);
        Task<List<IPExtension>> GetIPExtensionsOnOrderAsync(Int64 orderID);


        Task<List<Internet>> getInternetOnOrderAsync(long orderID);
        Task<List<Internet>> AddInternetToOrderAsync(Int64 orderID, List<Internet> Internet);
        Task<Internet> UpdateInternetOnOrderAsync(Int64 orderID, Internet Internet);
        Task<bool> RemoveInternetFromOrderAsync(long orderID, List<Internet> internets);
        Task<List<Contact>> GetContactsAsync(Int64 orderID);
        Task<List<Contact>> AddContactsAsync(Int64 orderID, List<Contact> contacts);
        Task<bool> RemoveContactsFromOrderAsync(Int64 orderID, List<Contact> contacts);


        Task<Mobile> AddMobileToOrderAsync(Int64 orderID, Mobile Mobile);
        Task<Mobile> UpdateMobileOnOrderAsync(Int64 orderID, Mobile Mobile);
        Task<bool> RemoveMobileFromOrderAsync(Int64 orderID, Int64 MobileID);

        Task<Inbound> AddInboundToOrderAsync(Int64 orderID, Inbound Inbound);
        Task<Inbound> UpdateInboundOnOrderAsync(Int64 orderID, Inbound Inbound);
        Task<bool> RemoveInboundFromOrderAsync(Int64 orderID, Int64 InboundID);

        Task<PAF> AddPAFToOrderAsync(Int64 orderID, PAF PAF);
        Task<PAF> UpdatePAFOnOrderAsync(Int64 orderID, PAF PAF);
        Task<bool> RemovePAFFromOrderAsync(Int64 orderID, Int64 PAFID);

        Task<List<Note>> AddNotesToOrderAsync(Int64 orderID, List<Note> Notes);
        Task<List<Note>>GetNotesOnOrderAsync(Int64 orderID, string Component = "");
        Task<Note> UpdateNotesOnOrderAsync(Int64 orderID, Note Notes);
        Task<bool> RemoveNotesFromOrderAsync(Int64 orderID, Int64 NotesID);

        Task<HardwareOrder> CreateHardwareOrderAsync(HardwareOrder hardware);
        Task<HardwareOrder> UpdateHardwareOrderAsync(HardwareOrder hardware);
        Task<HardwareOrder> DeleteHardwareOrderAsync(HardwareOrder hardware);
        Task<HardwareOrder> GetHardwareOrderAsync(Int64 hardwareOrderID);
        Task<List<Hardware>> getHandsetsOffOrder(Int64 orderID);

        Task<List<Hardware>> GetHardwareOnOrderAsync(Int64 orderID, bool OnlyIPVoice = false);
        Task<List<Hardware>> AddHardwareToOrderAsync(Int64 hardwareOrderID, List<Hardware> hardware);
        Task<bool> RemoveHardwareOnOrderAsync(Int64 orderID, List<Hardware> hardware);
        Task<bool> DeleteHardwareOnOrderAsync(Int64 hardwareOrderID);



        Task<bool> SendEmailAsync(Message email, ClaimsPrincipal dealer);
        Task<OrderView> GetOrderReport(Int64 orderID, Guid orderGUID = new Guid());
    }
}