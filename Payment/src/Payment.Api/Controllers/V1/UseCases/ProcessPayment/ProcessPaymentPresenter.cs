using Microsoft.AspNetCore.Mvc;
using Payment.Application.UseCases;

namespace Payment.Api.Controllers.V1
{
    public class ProcessPaymentPresenter : IProcessPaymentOutputPort
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
        public void OK(ProcessPaymentOutput output)
        {
            var response = new PaymentResponse(output);

            this.ViewModel = new OkObjectResult(response);
        }
    }
}
