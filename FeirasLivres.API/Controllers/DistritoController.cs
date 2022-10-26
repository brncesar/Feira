using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class DistritoController : BaseController
    {
        private readonly ILogger<DistritoController> _logger;
        private readonly FindDistrito _findDistritoUseCase;
        private readonly GetDistritoByCodigo _getDistritoByCodigo;

        public DistritoController(ILogger<DistritoController> logger, FindDistrito findDistritoUseCase, GetDistritoByCodigo getDistritoByCodigo)
        {
            _logger              = logger;
            _findDistritoUseCase = findDistritoUseCase;
            _getDistritoByCodigo = getDistritoByCodigo;
        }

        [HttpPost("Find")]
        public async Task<ActionResult> Find(FindDistritoParams findParam)
        {
            var findResult = await _findDistritoUseCase.Execute(findParam);

            return findResult.IsSuccess()
                ? Ok(findResult.Value)
                : Error(findResult);
        }

        [HttpPost("GetByCodigo")]
        public async Task<ActionResult> GetByCodigo(GetDistritoByCodigoParams codigo)
        {
            var domainResult = await _getDistritoByCodigo.Execute(codigo);

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }
    }
}