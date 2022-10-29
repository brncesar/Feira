using FeirasLivres.Api.Misc;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace FeirasLivres.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ObjectResult Error<T>(IDomainActionResult<T> domainActionResultWithError)
        {
            HttpContext.Items[CustomProblemDetailsFactory.HttpContextApplicationErrorsKey] = domainActionResultWithError.Errors;

            var firstError = domainActionResultWithError.Errors.First();

            var title = firstError.Description;

            var qtdErros = domainActionResultWithError.Errors.Count;
            if (qtdErros > 1)
                title = $"{title} Além de mais {qtdErros - 1} " +
                    $"erro{(qtdErros == 2 ? "" : "s")} " +
                    $"encontrado{(qtdErros == 2 ? "" : "s")}. " +
                    $"Verifique 'errorCodes' para mais detalhes.";

            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: title);
        }

        protected ObjectResult DomainResult<T>(IDomainActionResult<T> domainActionResult)
        {
            return domainActionResult.IsSuccess()
                ? Ok(domainActionResult.Value)
                : Error(domainActionResult);
        }
    }
}
