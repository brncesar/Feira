using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers;

public class SubPrefeituraController : BaseController<SubPrefeituraController>
{
    private readonly IFindSubPrefeitura        _findSubPrefeituraUseCase;
    private readonly IGetSubPrefeituraByCodigo _getSubPrefeituraByCodigo;

    public SubPrefeituraController(
        ILogger<SubPrefeituraController> logger,
        IFindSubPrefeitura findSubPrefeituraUseCase,
        IGetSubPrefeituraByCodigo getSubPrefeituraByCodigo)
        : base(logger)
    {
        _findSubPrefeituraUseCase = findSubPrefeituraUseCase;
        _getSubPrefeituraByCodigo = getSubPrefeituraByCodigo;
    }

    [HttpGet("Find")]
    public async Task<ActionResult> Find([FromQuery] FindSubPrefeituraParams findParam)
        => DomainResult(await _findSubPrefeituraUseCase.Execute(findParam));

    [HttpGet("GetByCodigo/{codigo}")]
    public async Task<ActionResult> GetByCodigo(string codigo)
        => DomainResult(await _getSubPrefeituraByCodigo.Execute(new(codigo)));
}