namespace App.Common.Connector
{
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    public class JsonContent<T> : StringContent
    {
        public JsonContent(string dataInJson) : base(dataInJson, Encoding.UTF8, "application/json")
        {
        }
        public JsonContent(T data) : base(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
        {
        }
    }
}