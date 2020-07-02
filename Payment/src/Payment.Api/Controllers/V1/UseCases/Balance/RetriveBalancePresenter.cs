namespace Payment.Api.Controllers.V1.UseCases.Wallet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Payment.Application.UseCases;

    /// <summary>
    /// RetriveBalancePresenter
    /// </summary>
    public class RetriveBalancePresenter : IRetriveBalanceOutputPort
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

        public void OK(RetriveBalanceOutput output)
        {
            this.ViewModel = new OkObjectResult(output);
        }
    }
}
