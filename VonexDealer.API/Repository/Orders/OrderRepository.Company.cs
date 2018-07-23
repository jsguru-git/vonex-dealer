using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public async Task<Company> AddCompanyToOrderAsync(long orderID, Company company)
        {
            company = await AddCompanyAsync(company);

            if (company != null)
            {
                Order order = GetOrders(orderID).FirstOrDefault();
                if (order != null)
                {
                    order.CustomerDetails.CompanyID = company.CompanyID;
                    _context.Update(order.CustomerDetails);
                    await _context.SaveChangesAsync();

                }

                return company;
            }
            else
                return null;
        }

        public async Task<bool> RemoveCompanyFromOrderAsync(long orderID, long CompanyID)
        {

            Order order = GetOrders(orderID).FirstOrDefault();

            if (order != null)
            {
                order.CustomerDetails.CompanyID = null;
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;

        }

        public async Task<Company> UpdateCompanyOnOrderAsync(long orderID, Company Company)
        {
            if (Company == null || Company.CompanyID == 0 || orderID == 0)
            {
                _logger.LogError($"UpdateCompanyAsync - Company does not exist");
                return null;
            }
            var Order = GetOrders(orderID).FirstOrDefault();
            try
            {
                if (Order != null)
                {
                    _context.Company.Update(Order.CustomerDetails.CompanyDetails);

                    await _context.SaveChangesAsync();
                    return Company;
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateCompanyAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<Company> AddCompanyAsync(Company Company)
        {
            _context.Company.Add(Company);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError($"unable to create company {ex.InnerException}");
                return null;
            }

            return Company;
           
        }
        public async Task<Company> UpdateCompanyAsync(Company Company)
        {
            _context.Company.Update(Company);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                _logger.LogError($"UpdateCompanyAsync: {ex.Message}");
            }

            return Company;
        }
        public async Task<bool> RemoveCompanyAsync(Int64 companyID)
        {
            Company comp = await _context.Company
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CompanyID == companyID);
            if (comp == null)
            {
                return false;
            }

            try
            {
                _context.Company.Remove(comp);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                _logger.LogError($"DeleteCompanyAsync: Error {ex.Message}");
                return false;
            }

        }
        public async Task<Company> GetCompanyAsync(Int64 companyID)
        {
            return await _context.Company.Where(x => x.CompanyID == companyID).FirstOrDefaultAsync();
        }

        public IQueryable<Company> GetCompanys()
        {
            return _context.Company.AsQueryable();
        }


    }
}
