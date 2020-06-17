using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Acquiring
{
    public class MockBankHttpMessageHandler : HttpMessageHandler
    {
        public enum Status { SUCCESSFUL = 1, UNSUCCESSFUL = 2 }

        public List<UnsuccessFullCardModel> _unSuccessFullCards;
     
        public MockBankHttpMessageHandler(List<UnsuccessFullCardModel> unSuccessFullCards)
        {
            _unSuccessFullCards = unSuccessFullCards;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                var input = await request.Content.ReadAsStringAsync();

                var cardPayment = JsonSerializer.Deserialize<CardPaymentRequest>(input);

                var _response = new
                {
                    PaymentId = cardPayment.PaymentId,
                    Status = (int)GetStatus(cardPayment.Card.CardNumber)
                };

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(_response))
                };
            }

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Bad Request")
            };
        }

        private Status GetStatus(string cardNumber)
        {
           if (_unSuccessFullCards.Exists(x=>x.CardNumber== cardNumber))
                return Status.UNSUCCESSFUL;

            return Status.SUCCESSFUL;
        }
    }
}
