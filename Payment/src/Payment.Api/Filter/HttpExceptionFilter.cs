
namespace Payment.Api.Filter
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Payment.Domain;

    public class HttpExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpExceptionFilter> _logger;


        /// <summary>
        /// constructor <see cref="HttpExceptionFilter" />
        /// </summary>
        /// <param name="logger"></param>
        public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context == null)
                return;

            if (context.Exception is ValueObjectException)
            {
                var exception = context.Exception as ValueObjectException;

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = exception.Details
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = "Payment gateway error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An unexpected error occured"
                };

                context.Result = new ObjectResult(problemDetails);
            }
        }
    }

}
