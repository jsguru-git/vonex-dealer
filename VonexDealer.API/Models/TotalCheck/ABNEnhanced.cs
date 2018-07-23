using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VonexDealer.API.Models.TotalCheck
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var data = AbnEnhanced.FromJson(jsonString);


    public partial class AbnEnhanced
    {

        [JsonProperty("result_count")]
        public long ResultCount { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("search_date")]
        public string SearchDate { get; set; }

        [JsonProperty("search_parameters")]
        public SearchParameters searchParameters { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_taken")]
        public long TimeTaken { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }


        public class SearchParameters
        {
            [JsonProperty("autocomplete")]
            [DefaultValue("false")]
            public string Autocomplete { get; set; }

            [JsonProperty("business_name")]
            public string BusinessName { get; set; }

            [JsonProperty("postcode")]
            public string Postcode { get; set; }

            [JsonProperty("rows")]
            [DefaultValue("10")]
            public string Rows { get; set; }

            [JsonProperty("search_type")]
            [DefaultValue("fast")]
            public string SearchType { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("abn_status")]
            [DefaultValue("Active")]
            public string ABNStatus { get; set; }

        }

        public partial class Result
        {
            [JsonProperty("type")]
            public string type { get; set; }
            [JsonProperty("abn")]
            public string Abn { get; set; }

            [JsonProperty("abn_id")]
            public string AbnId { get; set; }

            [JsonProperty("abn_status")]
            public string AbnStatus { get; set; }

            [JsonProperty("matched_name")]
            public string MatchedName { get; set; }

            [JsonProperty("matched_name_type")]
            public string MatchedNameType { get; set; }

            [JsonProperty("postcode")]
            public string Postcode { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }
        }

      
       
    }
    public partial class AbnEnhanced
    {
        public static AbnEnhanced FromJson(string json) => JsonConvert.DeserializeObject<AbnEnhanced>(json, Converter.Settings);
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

