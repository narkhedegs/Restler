using System.Collections.Generic;
using Newtonsoft.Json;
using RestApiTester.Common;

namespace Restler
{
    public class RestlerConfiguration
    {
        public RestlerConfiguration()
        {
            Environments = new List<Environment>();
        }

        [JsonProperty("collection")]
        public string CollectionFilePath { get; set; }

        [JsonProperty("parser")]
        public string ParserName { get; set; }

        [JsonProperty("environment")]
        public string EnvironmentName { get; set; }

        public string Interpolater { get; set; }
        public IList<Environment> Environments { get; set; }
        public IList<AddIn> AddIns { get; set; }
    }
}
