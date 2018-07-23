using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API.Models.TotalCheck
{
    public partial class AbnEnhancedID
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

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

        public partial class SearchParameters
        {
            [JsonProperty("search_type")]
            public string SearchType { get; set; }
        }
    }

    public partial class Result
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("abn")]
        public string Abn { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("abn_status")]
        public string AbnStatus { get; set; }

        [JsonProperty("entity_name")]
        public string EntityName { get; set; }

        [JsonProperty("entity_name_type")]
        public string EntityNameType { get; set; }

        [JsonProperty("abn_status_from_date")]
        public System.DateTime? AbnStatusFromDate { get; set; }

        [JsonProperty("entity_type_code")]
        public string EntityTypeCode { get; set; }

        [JsonProperty("entity_type")]
        public string EntityType { get; set; }

        [JsonProperty("entity_last_update_date")]
        public object EntityLastUpdateDate { get; set; }

        [JsonProperty("asic_number")]
        public string AsicNumber { get; set; }

        [JsonProperty("other_names")]
        public List<OtherName> OtherNames { get; set; }


        public partial class OtherName
        {
            [JsonProperty("other_name")]
            public string PurpleOtherName { get; set; }

            [JsonProperty("other_name_type")]
            public string OtherNameType { get; set; }
        }

       
    }
    public partial class AbnEnhancedID
    {
        public static AbnEnhancedID FromJson(string json) => JsonConvert.DeserializeObject<AbnEnhancedID>(json, Converter.Settings);
    }
    

}
