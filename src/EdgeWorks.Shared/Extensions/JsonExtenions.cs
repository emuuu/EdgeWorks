using Newtonsoft.Json;

namespace EdgeWorks.Shared.Extensions
{
    public static class JsonExtenions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
