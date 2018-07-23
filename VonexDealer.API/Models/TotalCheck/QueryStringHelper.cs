using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VonexDealer.API
{
    public class QueryStringHelper
    {
        public static string FormattedQueryString(Object searchParameters, string baseURI)
        {

            var paramDict = searchParameters.GetType()
                           .GetProperties()
                           .ToDictionary(x => x.GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                             .Cast<JsonPropertyAttribute>().FirstOrDefault().PropertyName
                             , x => x.GetValue(searchParameters, null));

            List<KeyValuePair<string, string>> paramPairs = new List<KeyValuePair<string, string>>();
            foreach (var item in paramDict.Where(x => x.Value != null))
            {
                paramPairs.Add(new KeyValuePair<string, string>(item.Key.ToString(), item.Value.ToString()));
            }

            return $"{baseURI}{QueryString.Create(paramPairs).ToString()}";



        }
    }
}
