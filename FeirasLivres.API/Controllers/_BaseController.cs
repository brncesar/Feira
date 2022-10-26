using ErrorOr;
using FeirasLivres.Api.Misc;
using FeirasLivres.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace FeirasLivres.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult Error<T>(IDomainActionResult<T> domainActionResultWithError) {
            HttpContext.Items[CustomProblemDetailsFactory.HttpContextApplicationErrorsKey] = domainActionResultWithError.Errors;

            var firstError = domainActionResultWithError.Errors.First();

            var title = firstError.Description;
            var statusCode = firstError.Type switch {
                ErrorType.Conflict   => StatusCodes.Status409Conflict,
                ErrorType.NotFound   => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _                    => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: title);
        }
    }
}
