using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using Xunit;
namespace VonexDealer.API.Tests.Orders
{
    public partial class OrderTests
    {

        [Fact(DisplayName = "Order Add Company")]
        public async void AddCompanyToOrder()
        {
            Order findOrder = _order.GetOrders(0).Where(x => x.CustomerDetails.CompanyDetails == null).FirstOrDefault();
            Assert.NotNull(findOrder);
            Company company = new Company { CompanyName = "ALSConsulting", ABN = "12345657", DirectorGivenName = "Andrew", DirectorSurname = "Smith" };
            Company updatedOrder = await _order.AddCompanyToOrderAsync(findOrder.OrderID, company);

            Assert.Equal(company.CompanyID, updatedOrder.CompanyID);


        }

        [Fact(DisplayName = "Order update Company")]
        public async void UpdateCompanyToOrder()
        {
            Order findOrder = _order.GetOrders(0).Where(x => x.CustomerDetails.CompanyDetails != null).FirstOrDefault();
            Assert.NotNull(findOrder);
            
            Company company = findOrder.CustomerDetails.CompanyDetails;
            company.CompanyName = "George";
            company.ABN = "12345678912";
            company.DirectorGivenName = "Amdrew";
            company.DirectorSurname = "Smith";

            Company UpdatedOrder = await _order.UpdateCompanyOnOrderAsync(findOrder.OrderID, company);

            Assert.NotNull(UpdatedOrder);

            Assert.Equal(company.CompanyName, UpdatedOrder.CompanyName);
            
        }

        [Fact(DisplayName = "Order delete Company")]
        public async void DeleteCompanyFromOrder()
        {
            Order findOrder = _order.GetOrders(0).Where(x => x.CustomerDetails.CompanyDetails != null).FirstOrDefault();
            Assert.NotNull(findOrder);
            Int64 origID = findOrder.OrderID;
            Int64 origCompID = findOrder.CustomerDetails.CompanyDetails.CompanyID;
            Company company = new Company { CompanyName = "ALSConsulting", ABN = "12345657", DirectorGivenName = "Andrew", DirectorSurname = "Smith" };

            Assert.NotNull(findOrder.CustomerDetails.CompanyDetails);

            bool updatedOrder = await _order.RemoveCompanyFromOrderAsync(origID, origCompID);

            Assert.True(updatedOrder);



        }


        [Fact(DisplayName = "Add Company")]
        public async void AddCompany()
        {

            
            Company newCompany = await _order.AddCompanyAsync(new Company { CompanyName = "ALSConsulting", ABN = "12345657", DirectorGivenName = "Andrew", DirectorSurname = "Smith" });

            Assert.True(newCompany.CompanyID > 0);


        }

        [Fact(DisplayName = "update Company")]
        public async void UpdateCompany()
        {

            Int64 companyID = _order.GetCompanys().Select(x => x.CompanyID).FirstOrDefault();

            Company company = await _order.GetCompanyAsync(companyID);
            Assert.NotNull(company);

            string oldName = company.CompanyName;

            company.CompanyName = "this is a test";

            Company newCompany = await _order.UpdateCompanyAsync( company);
          

            Assert.NotEqual(oldName, newCompany.CompanyName);

            company.CompanyName = oldName;
             await _order.UpdateCompanyAsync(company);

            Assert.Equal(oldName, newCompany.CompanyName);

        }

        [Fact(DisplayName = "delete Company")]
        public async void DeleteCompany()
        {
            //get first company
            Int64 companyID = _order.GetCompanys().Select(x => x.CompanyID).FirstOrDefault();

           
           bool result = await _order.RemoveCompanyAsync(companyID);
            

            Assert.True(result);



        }


    }
}
