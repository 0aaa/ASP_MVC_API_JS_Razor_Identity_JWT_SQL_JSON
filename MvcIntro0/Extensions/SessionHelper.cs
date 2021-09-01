using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Extensions
{
    public static class SessionHelper
    {
        public static void Serialize(this ISession session, string key, object value)
            => session.SetString(key, JsonConvert.SerializeObject(value));


        public static GenType Deserialize<GenType>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<GenType>(value);
        }
    }
}
