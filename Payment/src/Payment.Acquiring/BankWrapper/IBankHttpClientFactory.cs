using System.Net.Http;

namespace Payment.Acquiring
{
    public interface IBankHttpClientFactory
    {
        HttpClient CreateHttpClient();
    }
}