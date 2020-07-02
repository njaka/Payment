namespace Payment.Api.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentMediator;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Payment.Api.Controllers.V1.UseCases.Wallet;
    using Payment.Application.UseCases;
    using Payment.Application.UseCases.RetriveBalance;

    /// <summary>
    /// Payment Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1.0/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RetriveBalancePresenter _retriveBalancePresenter;

        public WalletController(
          IMediator mediator,
          RetriveBalancePresenter retriveBalancePresenter)
        {
            _mediator = mediator;
            _retriveBalancePresenter = retriveBalancePresenter;
        }

        /// <summary>
        /// Retrieve Payment Detail
        /// </summary>
        /// <param name="beneficiaryAlias">beneficiaryAlias</param>
        /// <returns></returns>
        [HttpGet]
        [Route("balance/{beneficiaryAlias}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RetriveBalanceOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBalance(string beneficiaryAlias)
        {
            var input = new RetriveBalanceInput()
            {
                 BeneficiaryAlias = beneficiaryAlias
            };

            await _mediator.PublishAsync(input);

            return _retriveBalancePresenter.ViewModel;
        }
    }
}
