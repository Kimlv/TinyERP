namespace App.Common.Connector
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    public class JsonContent<T> : StringContent
    {
        public JsonContent(T data) : base(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
        {
        }
    }
}