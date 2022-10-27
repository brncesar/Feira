using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class FeiraController : BaseController
    {
        private readonly ILogger<FeiraController> _logger;
        private readonly IFeiraRepository         _feiraRepository;
        private readonly FindFeira                _findFeiraUseCase;
        private readonly AddNewFeira              _addNewFeiraUseCase;
        private readonly EditExistingFeira        _editFeiraUseCase;
        private readonly RemoveExistingFeira      _removeFeiraUseCase;

        public FeiraController(
            ILogger<FeiraController> logger,
            IFeiraRepository         feiraRepository,
            FindFeira                findFeiraUseCase,
            AddNewFeira              addNewFeiraUseCase,
            EditExistingFeira        editFeiraUseCase,
            RemoveExistingFeira      removeFeiraUseCase)
        {
            _logger             = logger;
            _feiraRepository    = feiraRepository;
            _findFeiraUseCase   = findFeiraUseCase;
            _addNewFeiraUseCase = addNewFeiraUseCase;
            _editFeiraUseCase   = editFeiraUseCase;
            _removeFeiraUseCase = removeFeiraUseCase;
        }

        [HttpGet("Find")]
        public async Task<ActionResult> Find([FromQuery] FindFeiraParams findParams)
        {
            var findResult = await _findFeiraUseCase.Execute(findParams);

            return findResult.IsSuccess()
                ? Ok(findResult.Value)
                : Error(findResult);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(AddNewFeiraParams addParams)
        {
            var domainResult = await _addNewFeiraUseCase.Execute(addParams);

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(EditExistingFeiraParams editParams)
        {
            var domainResult = await _editFeiraUseCase.Execute(editParams);

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }

        [HttpDelete("Remove/{numeroRegistro}")]
        public async Task<ActionResult> Remove(string numeroRegistro)
        {
            var domainResult = await _removeFeiraUseCase.Execute(new(numeroRegistro));

            return domainResult.IsSuccess()
                ? Ok(domainResult.Value)
                : Error(domainResult);
        }
    }
}