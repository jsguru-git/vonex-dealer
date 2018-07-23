using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public Task<Mobile> AddMobileToOrderAsync(long orderID, Mobile Mobile)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RemoveMobileFromOrderAsync(long orderID, long MobileID)
        {
            throw new NotImplementedException();
        }

        public Task<Mobile> UpdateMobileOnOrderAsync(long orderID, Mobile Mobile)
        {
            throw new NotImplementedException();
        }
    }
}
