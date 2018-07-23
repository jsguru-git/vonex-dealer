using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VonexDealer.API.Models.TotalCheck
{

    public partial class TCAddress
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("search_date")]
        public string SearchDate { get; set; }

        [JsonProperty("search_parameters")]
        public SearchParameters searchParameters { get; set; }

        [JsonProperty("time_taken")]
        public long TimeTaken { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("result_count")]
        public long ResultCount { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }




        public partial class Result
        {
            [JsonProperty("@type")]
            public string PurpleType { get; set; }

            [JsonProperty("index")]
            public long Index { get; set; }

            [JsonProperty("search_result_id")]
            public string SearchResultId { get; set; }

            [JsonProperty("listing_type")]
            public object ListingType { get; set; }

            [JsonProperty("formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty("postcode")]
            public string Postcode { get; set; }

            [JsonProperty("state")]
            public object State { get; set; }

            [JsonProperty("street_address")]
            public string StreetAddress { get; set; }

            [JsonProperty("suburb")]
            public string Suburb { get; set; }

            [JsonProperty("city")]
            public object City { get; set; }

            [JsonProperty("country")]
            public object Country { get; set; }

            [JsonProperty("primary_name")]
            public object PrimaryName { get; set; }

            [JsonProperty("secondary_name")]
            public object SecondaryName { get; set; }

            [JsonProperty("is_postal")]
            public bool IsPostal { get; set; }

            [JsonProperty("is_listing")]
            public bool IsListing { get; set; }

            [JsonProperty("contains_subpremises")]
            public bool ContainsSubpremises { get; set; }
        }

        public partial class SearchParameters
        {
            [JsonProperty("country")]
            [DefaultValue("AU")]
            public string Country { get; set; }

            [JsonProperty("formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty("include_paf")]
            public string IncludePaf { get; set; }

            [JsonProperty("include_listing")]
            public string IncludeListing { get; set; }
        }

      

        public class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            };



        }
    }
    public partial class TCAddress
    {
        public static TCAddress FromJson(string json) => JsonConvert.DeserializeObject<TCAddress>(json, Converter.Settings);

    }




}
