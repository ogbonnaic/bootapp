using System;
using Newtonsoft.Json.Linq;

namespace Bootapp.Util
{
    public class JsonUtil
    {
       public static string GetString(JObject jObject,string key)
        {
            var result = jObject[key];
            if (result != null)
                return result.ToString();
            return "";
        }
    }
}
