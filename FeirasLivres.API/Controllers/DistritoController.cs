using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class DistritoController : BaseController
    {
        private readonly ILogger<DistritoController> _logger;
        private readonly FindDistrito _findDistritoUseCase;

        public DistritoController(ILogger<DistritoController> logger, FindDistrito findDistritoUseCase)
        {
            _logger              = logger;
            _findDistritoUseCase = findDistritoUseCase;
        }

        [HttpGet(Name = "Find")]
        public async Task<ActionResult> Get(FindDistritoParams findParam)
        {
            var findResult = await _findDistritoUseCase.Execute(findParam);

            return findResult.IsSuccess()
                ? Ok(findResult.Value)
                : Error(findResult);
        }
    }
}