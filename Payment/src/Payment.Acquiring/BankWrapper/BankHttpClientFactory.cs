using Microsoft.Extensions.Options;
using Payment.Acquiring.Configuration;
using System.Net.Http;

namespace Payment.Acquiring
{
    public class BankHttpClientFactory : IBankHttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly bool _useMockBank;
        public BankHttpClientFactory(IHttpClientFactory httpClientFactory, IOptions<FeaturesModel> config)
        {
            _httpClientFactory = httpClientFactory;
            _useMockBank = config.Value.UseBankMock;
        }
        public HttpClient CreateHttpClient()
        {
            if (_useMockBank)
                return _httpClientFactory.CreateClient(Constants.MOCK_BANK);

            return _httpClientFactory.CreateClient(Constants.REAL_BANK);
        }
    }
}
