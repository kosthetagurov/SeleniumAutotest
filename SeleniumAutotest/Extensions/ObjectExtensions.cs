using Newtonsoft.Json;

namespace SeleniumAutotest.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this Object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
