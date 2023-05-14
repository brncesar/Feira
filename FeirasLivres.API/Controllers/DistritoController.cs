using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers;

public class DistritoController : BaseController<DistritoController>
{
    private readonly IFindDistrito        _findDistritoUseCase;
    private readonly IGetDistritoByCodigo _getDistritoByCodigo;

    public DistritoController(
        ILogger<DistritoController> logger,
        IFindDistrito findDistritoUseCase,
        IGetDistritoByCodigo getDistritoByCodigo)
        : base(logger)
    {
        _findDistritoUseCase = findDistritoUseCase;
        _getDistritoByCodigo = getDistritoByCodigo;
    }

    [HttpGet("Find")]
    public async Task<ActionResult> Find([FromQuery] FindDistritoParams findParam)
        => DomainResult(await _findDistritoUseCase.Execute(findParam));

    [HttpGet("GetByCodigo/{codigo}")]
    public async Task<ActionResult> GetByCodigo(string codigo)
        => DomainResult(await _getDistritoByCodigo.Execute(new(codigo)));
}