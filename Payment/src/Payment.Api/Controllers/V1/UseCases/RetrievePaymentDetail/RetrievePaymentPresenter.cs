using Microsoft.AspNetCore.Mvc;
using Payment.Application.UseCases;

namespace Payment.Api.Controllers.V1.RetrievePaymentDetail
{
    public class RetrievePaymentPresenter : IRetriePaymentOutputPort
    {
        public IActionResult ViewModel { get; private set; }

        public void BadRequest(string message)
        {
            this.ViewModel = new BadRequestObjectResult(message);
        }

        public void NotFound(string message)
        {
            this.ViewModel = new NotFoundObjectResult(message);
        }

        public void OK(Domain.Payment output)
        {
            var response = new RetrievePaymentDetailResponse(output);

            this.ViewModel = new OkObjectResult(response);
        }
    }
}
