using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InputScrubbingModelBinder.Web.Example
{
    // Credit for the following goes to Ben Cull @ http://benjii.me/2015/07/using-sessions-and-httpcontext-in-aspnet5-and-mvc6/
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
