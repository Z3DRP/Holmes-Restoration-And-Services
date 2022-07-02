
using Newtonsoft.Json;

namespace Holmes_Services.Models.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject<T>(this ISession session, string key, T value) =>
           session.SetString(key, JsonConvert.SerializeObject(value));

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
