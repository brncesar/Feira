using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class SubPrefeituraController : BaseController<SubPrefeituraController>
    {
        private readonly FindSubPrefeitura        _findSubPrefeituraUseCase;
        private readonly GetSubPrefeituraByCodigo _getSubPrefeituraByCodigo;

        public SubPrefeituraController(
            ILogger<SubPrefeituraController> logger,
            FindSubPrefeitura findSubPrefeituraUseCase,
            GetSubPrefeituraByCodigo getSubPrefeituraByCodigo)
            : base(logger)
        {
            _findSubPrefeituraUseCase = findSubPrefeituraUseCase;
            _getSubPrefeituraByCodigo = getSubPrefeituraByCodigo;
        }

        [HttpGet("Find")]
        public async Task<ActionResult> Find([FromQuery] FindSubPrefeituraParams findParam)
        {
            var domainResult = await _findSubPrefeituraUseCase.Execute(findParam);

            return DomainResult(domainResult);
        }

        [HttpGet("GetByCodigo/{codigo}")]
        public async Task<ActionResult> GetByCodigo(string codigo)
        {
            var domainResult = await _getSubPrefeituraByCodigo.Execute(new(codigo));

            return DomainResult(domainResult);
        }
    }
}