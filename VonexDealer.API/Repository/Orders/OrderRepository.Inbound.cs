using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public Task<Inbound> AddInboundToOrderAsync(long orderID, Inbound Inbound)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RemoveInboundFromOrderAsync(long orderID, long InboundID)
        {
            throw new NotImplementedException();
        }
        public Task<Inbound> UpdateInboundOnOrderAsync(long orderID, Inbound Inbound)
        {
            throw new NotImplementedException();
        }
    }
}
