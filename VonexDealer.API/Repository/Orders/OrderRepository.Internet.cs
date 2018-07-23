using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {

        public async Task<List<Internet>> getInternetOnOrderAsync(long orderID)
        {
            if (orderID == 0) return null;
            List<Internet> internets = await _context.Internet.Where(x => x.OrderID == orderID).AsNoTracking().ToListAsync();

            return internets;

        }

        public async Task<List<Internet>> AddInternetToOrderAsync(long orderID, List<Internet> internets)
        {

            if (orderID == 0 || internets.Count == 0)
                return null;

            foreach (Internet net in internets)
            {
                net.OrderID = orderID;

                if (net.InternetID > 0)
                {
                    _context.Internet.Update(net);
                }
                else
                {
                    _context.Internet.Add(net);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return internets;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }

        }



        public async Task<bool> RemoveInternetFromOrderAsync(long orderID, List<Internet> internets)
        {
            if (orderID == 0 || internets.Count == 0)
            {
                return false;
            }

            foreach (var internet in internets)
            {
                _context.Internet.Remove(internet);
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }

        }

        public async Task<bool> RemoveContactsFromOrderAsync(Int64 orderID, List<Contact> contacts)
        {
            if(orderID == 0 || contacts.Count == 0)
            {
                return false;
            }

            foreach (var contact in contacts)
            {
                _context.Contacts.Remove(contact);
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return false;
            }

        }
        public Task<Internet> UpdateInternetOnOrderAsync(long orderID, Internet Internet)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contact>> GetContactsAsync(Int64 orderID)
        {
            List<Contact> contacts = await _context.Contacts.Where(x => x.OrderID == orderID).ToListAsync();

            if(contacts.Count > 0)
            return contacts;

            Contact contact = await _context.Customers
                 .Join(_context.Orders, c => c.CustomerID, o => o.CustomerID, (c, o) => new
                 Contact{
                     ContactEmail = c.Email,
                     ContactMobile = c.Mobile,
                     ContactName = $"{c.FirstName} {c.Surname}",
                     OrderID = o.OrderID
                 })
                 .Where(x => x.OrderID == orderID)
                 .FirstOrDefaultAsync();

            if (contact == null)
                return null;

            List<Contact> newContacts = new List<Contact>();
            newContacts.Add(contact);

            return await AddContactsAsync(orderID, newContacts);

                

           
        }

        public async Task<List<Contact>> AddContactsAsync(Int64 orderID, List<Contact> contacts)
        {
            if (orderID == 0 || contacts.Count == 0)
                return null;

            foreach (Contact contact in contacts)
            {
                contact.OrderID = orderID;

                if (contact.ContactID > 0)
                {
                    _context.Contacts.Update(contact);
                }
                else
                {
                    _context.Contacts.Add(contact);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return contacts;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.InnerException.ToString());
                return null;
            }
        }

    }
}
