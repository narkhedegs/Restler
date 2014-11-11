using Newtonsoft.Json;

namespace Restler
{
    public class AddIn
    {
        public string Name { get; set; }

        [JsonConverter(typeof(AddInConfigurationConverter))]
        public string Configuration { get; set; }
    }
}
