using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.TotalCheck;

namespace VonexDealer.API.Repository
{
    public interface ITCRepository
    {
        Task<TCAddress> getAddressesAsync(TCAddress.SearchParameters searchParameters);
        Task<AbnEnhanced> getABNsAsync(AbnEnhanced.SearchParameters searchParameters);
        Task<AbnEnhancedID> getABNByIDAsync(String abnOrID);
    }
}
