using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Dispatching.Aaa.Tests
{
    public static class ObjectExtensions
    {
        public static HttpResponseMessage ToHttpResponseMessage<T>(this T input) where T : class
        {
            var jsonString = JsonConvert.SerializeObject(input);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return new HttpResponseMessage
            {
                Content = content
            };
        }


        public static HttpResponseMessage ToHttpResponseMessage<T>(this T input, HttpStatusCode statusCode) where T : class
        {
            var jsonString = JsonConvert.SerializeObject(input);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return new HttpResponseMessage(statusCode)
            {
                Content = content
            };
        }
    }
}
