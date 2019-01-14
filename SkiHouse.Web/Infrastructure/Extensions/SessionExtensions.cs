using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace SkiHouse.Web.Infrastructure.Extensions
{
    public static class SessionExtensions
    {
        public static T GetJson<T>(this ISession session, string key)
        {
            var data = session.GetString(key);

            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetJson(this ISession session, string key, object value) =>
            session.SetString(key, JsonConvert.SerializeObject(value));
    }
}
