using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VonexDealer.API.Data;
using System.ServiceModel;
using wsUtbCustomer;
using VonexDealer.API.Models.Order;
using System;
using VonexDealer.API.Models.UB;
using System.Linq;

namespace VonexDealer.API.Repository
{
    public class UtilibillRepository : IUtilibill
    {
        private ILogger<UtilibillRepository> _logger;
        private IConfiguration _config;
        private ApplicationDbContext _context;
        private UBEnvironment _ubEnvironment;


        public UtilibillRepository(ILogger<UtilibillRepository> logger, IConfiguration config, ApplicationDbContext context)
        {
            _logger = logger;
            _config = config;
            _context = context;
            _ubEnvironment = _context.UBEnvironments.Where(x => x.environment == $"{_config["Utilibill:environment"]}").FirstOrDefault();

            

        }

        public async Task<CustomerResponse> GetCustomerAsync(string custNo)
        {
            
            UtbCustomerClient utbCustomerClient = new UtbCustomerClient();
            utbCustomerClient.Endpoint.Address = new EndpointAddress($"{ _config["Utilibill:URL"]}UtbCustomer");
            wsrCustomer wsrCustomer = new wsrCustomer();
            wsCustomer wsCustomer = new wsCustomer();
            getCustomerResponse response = new getCustomerResponse();
            wsLogin wslogin = new wsLogin();
            wslogin.userName = _ubEnvironment.UserName;
            wslogin.password = _ubEnvironment.Password;

            wsCustomer.custNo = custNo;
            try
            {
                response = await utbCustomerClient.getCustomerAsync(wsCustomer, wslogin);
            }
            catch (FaultException ex)
            {
                _logger.LogError(ex.ToString());
                return new CustomerResponse { Result = ex.Reason.ToString(), customer = null };
            }
            return new CustomerResponse { Result = "OK", customer = response.@return };
        }

        public async Task<CustomerResponse> AddCustomerAsync(Customer cust)
        {
            UtbCustomerClient utbCustomerClient = new UtbCustomerClient();
            utbCustomerClient.Endpoint.Address = new EndpointAddress($"{ _config["Utilibill:URL"]}UtbCustomer");
            wsrCustomer wsrCustomer = new wsrCustomer();
            addCustomerResponse response = new addCustomerResponse();
            wsLogin wslogin = new wsLogin();
            wslogin.userName = _ubEnvironment.UserName;
            wslogin.password = _ubEnvironment.Password;
            //mandatory customer fields


            //map cust fields to customer
            wsCustomer customer = MapCustomer(cust);
            customer.custNo = "0";
            customer.groupNo = "1";
            try
            {
                response = await utbCustomerClient.addCustomerAsync(customer, wslogin);
            }
            catch (FaultException ex)
            {
                _logger.LogError(ex.ToString());
                return new CustomerResponse { Result = ex.Message + response.@return.errorDescription, customer = null };
            }
            return new CustomerResponse { Result = "OK", customer = response.@return };
        }

        private wsCustomer MapCustomer(Customer cust)
        {

            wsCustomer customer = new wsCustomer
            {
                sal = cust.Salutation,
              
                email = cust.Email,
                firstName = cust.FirstName,
                mobile = cust.Mobile ?? cust.Phone,
                phone = cust.Phone ?? cust.Mobile,
               
                surname = cust.Surname,
                agent_id = cust.DealerID.ToString()
                
            };
            if(cust.CompanyDetails != null)
            {
                customer.company = cust.CompanyDetails != null ? cust.CompanyDetails.CompanyName : null;
                customer.abn = cust.CompanyDetails.ABN;
                customer.acn = cust.CompanyDetails.ACN;
            }

            if (cust.AddressDetails != null)
            {
                int i = 0;
                if (cust.AddressDetails.Count > 0)
                {
                    if(cust.AddressDetails[i].PostCode == 0 || cust.AddressDetails[i].State == null || cust.AddressDetails[i].Suburb == null)
                    {
                        i += 1;
                    }
                    customer.postcode = cust.AddressDetails[i].PostCode.ToString();
                    customer.address = cust.AddressDetails[i].StreetAddress;
                    customer.suburb = cust.AddressDetails[i].Suburb;
                    customer.tradingName = cust.CompanyDetails == null ? "" : cust.CompanyDetails.TradingName;
                    customer.state = cust.AddressDetails[i].State;
                }
            }
            return customer;
        }

        public async Task<CustomerResponse> EditCustomerAsync(wsCustomer customer)
        {
            UtbCustomerClient utbCustomerClient = new UtbCustomerClient();
            utbCustomerClient.Endpoint.Address = new EndpointAddress($"{ _config["Utilibill:URL"]}UtbCustomer");

            //must have a valid customer number.
            if (customer.custNo == null)
                return null;

            wsrCustomer wsrCustomer = new wsrCustomer();
            updateCustomerResponse response = new updateCustomerResponse();
            wsLogin wslogin = new wsLogin();
            wslogin.userName = _ubEnvironment.UserName;
            wslogin.password = _ubEnvironment.Password;
            //mandatory customer fields


            try
            {
                response = await utbCustomerClient.updateCustomerAsync(customer, wslogin);
            }
            catch (FaultException ex)
            {
                _logger.LogError(ex.ToString());
                return new CustomerResponse { Result = response.@return.errorDescription, customer = null };
            }
            return new CustomerResponse { Result = "OK", customer = response.@return };
        }
    }




}


public class CustomerResponse
{
    public string Result { get; set; }
    public wsrCustomer customer { get; set; }
}

