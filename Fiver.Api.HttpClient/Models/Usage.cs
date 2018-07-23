using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fiver.Api.HttpClient.Models
{
    public class Usage
    {
        public string Name { get; set; }
        public int Extensions { get; set; }
        [JsonProperty(PropertyName = "active-extensions")]
        public int ActiveExtensions { get; set; }
        public int Acd { get; set; }
        public int Conf { get; set; }
        public string Parm1 { get; set; }
        public string Parm2 { get; set; }
        public string Parm3 { get; set; }
    }
}
