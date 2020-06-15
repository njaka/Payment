namespace Payment.Acquiring
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    public class BankClient : IBankClient
    {
        private readonly HttpClient _httpClient;

        public BankClient(IBankHttpClientFactory bankHttpClientFactory)
        {
            _httpClient = bankHttpClientFactory.CreateHttpClient();
        }

        public async Task<CardPaymentResponse> CreateCardPayment(CardPaymentRequest payment)
        {
            var serializePayment = JsonSerializer.Serialize(payment);

            var httpContent = new StringContent(serializePayment, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("cardpayment", httpContent);

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CardPaymentResponse>(stringResponse);

            return result;
        }
    }
}
