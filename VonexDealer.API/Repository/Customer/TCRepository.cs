using Fiver.Api.HttpClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.TotalCheck;

namespace VonexDealer.API.Repository
{
    public class TCRepository : ITCRepository
    {
        private IConfiguration _config;
       
        public TCRepository(IConfiguration config)
        {
            _config = config;
           
        }
        private async Task<string> GetQuery(string query)
        {

           
            string address = _config["AddressLookup:Prod:TC_EndPoint"];
            string username = _config["TotalCheck:username"];
            string password = _config["TotalCheck:password"];

           
            string authInfo = $"{username}:{password}";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            address += "/" + query; 

                var response = await HttpRequestFactory.Get(address, "Basic", authInfo);

            if (response.IsSuccessStatusCode)
            {
                string result = response.ContentAsString();

                return result;
            }
            else
            {
                string result = JsonConvert.SerializeObject(new { Status = "Fail", Reason = response.ReasonPhrase });
                return result;
            }


        }

        public async Task<TCAddress> getAddressesAsync(TCAddress.SearchParameters searchParameters)
        {
            string result = await GetQuery(QueryStringHelper.FormattedQueryString(searchParameters, "address"));
            var addressInfo = TCAddress.FromJson(result);
           
            return addressInfo;
        }

        public async Task<AbnEnhanced> getABNsAsync(AbnEnhanced.SearchParameters searchParameters)
        {
            string result = await GetQuery(QueryStringHelper.FormattedQueryString(searchParameters, "abn_enhanced"));
            var addressInfo = AbnEnhanced.FromJson(result);

            return addressInfo;
        }

        public async Task<AbnEnhancedID> getABNByIDAsync(String abn)
        {
            string result = await GetQuery($"abn_enhanced/{abn.Replace(" ","")}?search_type=live");
            AbnEnhancedID addressInfo = AbnEnhancedID.FromJson(result);

            return addressInfo;
        }
    }
}
