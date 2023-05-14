using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers;

public class FeiraController : BaseController<FeiraController>
{
    private readonly IFindFeira           _findFeiraUseCase;
    private readonly IAddNewFeira         _addNewFeiraUseCase;
    private readonly IEditExistingFeira   _editFeiraUseCase;
    private readonly IRemoveExistingFeira _removeFeiraUseCase;

    public FeiraController(
        ILogger<FeiraController> logger,
        IFindFeira                findFeiraUseCase,
        IAddNewFeira              addNewFeiraUseCase,
        IEditExistingFeira        editFeiraUseCase,
        IRemoveExistingFeira      removeFeiraUseCase)
        : base(logger)
    {
        _findFeiraUseCase   = findFeiraUseCase;
        _addNewFeiraUseCase = addNewFeiraUseCase;
        _editFeiraUseCase   = editFeiraUseCase;
        _removeFeiraUseCase = removeFeiraUseCase;
    }

    [HttpGet("Find")]
    public async Task<ActionResult> Find([FromQuery] FindFeiraParams findParams)
        => DomainResult(await _findFeiraUseCase.Execute(findParams));

    [HttpPost("Add")]
    public async Task<ActionResult> Add(AddNewFeiraParams addParams)
        => DomainResult(await _addNewFeiraUseCase.Execute(addParams));

    [HttpPut("Edit")]
    public async Task<ActionResult> Edit(EditExistingFeiraParams editParams)
        => DomainResult(await _editFeiraUseCase.Execute(editParams));

    [HttpDelete("Remove/{numeroRegistro}")]
    public async Task<ActionResult> Remove(string numeroRegistro)
        => DomainResult(await _removeFeiraUseCase.Execute(new(numeroRegistro)));
}