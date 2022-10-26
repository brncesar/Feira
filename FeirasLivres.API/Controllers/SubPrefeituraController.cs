using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class SubPrefeituraController : BaseController
    {
        private readonly ILogger<SubPrefeituraController> _logger;
        private readonly FindSubPrefeitura                _findSubPrefeituraUseCase;
        private readonly GetSubPrefeituraByCodigo         _getSubPrefeituraByCodigo;

        public SubPrefeituraController(ILogger<SubPrefeituraController> logger, FindSubPrefeitura findSubPrefeituraUseCase, GetSubPrefeituraByCodigo getSubPrefeituraByCodigo)
        {
            _logger              = logger;
            _findSubPrefeituraUseCase = findSubPrefeituraUseCase;
            _getSubPrefeituraByCodigo = getSubPrefeituraByCodigo;
        }

        [HttpPost("Find")]
        public async Task<ActionResult> Find(FindSubPrefeituraParams findParam)
        {
            var findResult = await _findSubPrefeituraUseCase.Execute(findParam);

            return findResult.IsSuccess()
                ? Ok(findResult.Value)
                : Error(findResult);
        }

        [HttpPost("GetByCodigo")]
        public async Task<ActionResult> GetByCodigo(GetSubPrefeituraByCodigoParams codigo)
        {
            var domainResult = await _getSubPrefeituraByCodigo.Execute(codigo);

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }
    }
}