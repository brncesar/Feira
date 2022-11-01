using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers;

public class DistritoController : BaseController<DistritoController>
{
    private readonly FindDistrito        _findDistritoUseCase;
    private readonly GetDistritoByCodigo _getDistritoByCodigo;

    public DistritoController(
        ILogger<DistritoController> logger,
        FindDistrito findDistritoUseCase,
        GetDistritoByCodigo getDistritoByCodigo)
        : base(logger)
    {
        _findDistritoUseCase = findDistritoUseCase;
        _getDistritoByCodigo = getDistritoByCodigo;
    }

    [HttpGet("Find")]
    public async Task<ActionResult> Find([FromQuery] FindDistritoParams findParam)
    {
        var domainResult = await _findDistritoUseCase.Execute(findParam);

        return DomainResult(domainResult);
    }

    [HttpGet("GetByCodigo/{codigo}")]
    public async Task<ActionResult> GetByCodigo(string codigo)
    {
        var domainResult = await _getDistritoByCodigo.Execute(new(codigo));

        return DomainResult(domainResult);
    }
}