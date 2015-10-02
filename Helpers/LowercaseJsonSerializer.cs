using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace dsc.CalendarWidget.Helpers {
    public class LowercaseJsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, Settings);
        }
    }
}