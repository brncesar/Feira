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

        [HttpGet("Find")]
        public async Task<ActionResult> Find([FromQuery] FindDistritoParams findParam)
        {
            var findResult = await _findDistritoUseCase.Execute(findParam);

            return findResult.IsSuccess()
                ? Ok(findResult.Value)
                : Error(findResult);
        }

        [HttpGet("GetByCodigo/{codigo}")]
        public async Task<ActionResult> GetByCodigo(string codigo)
        {
            var domainResult = await _getDistritoByCodigo.Execute(new(codigo));

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }
    }
}