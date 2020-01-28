using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
        Task<string> GetStringAsync(string requestUri);
    }

    public class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
    }
}
