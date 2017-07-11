namespace App.Common.Helpers
{
    using Newtonsoft.Json;
    public class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
