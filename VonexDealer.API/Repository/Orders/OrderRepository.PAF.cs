using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {
        public Task<PAF> AddPAFToOrderAsync(long orderID, PAF PAF)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePAFFromOrderAsync(long orderID, long PAFID)
        {
            throw new NotImplementedException();
        }

        public Task<PAF> UpdatePAFOnOrderAsync(long orderID, PAF PAF)
        {
            throw new NotImplementedException();
        }
    }
}
