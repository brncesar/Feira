using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class FeiraController : BaseController
    {
        private readonly ILogger<FeiraController> _logger;
        private readonly IFeiraRepository         _feiraRepository;
        private readonly AddNewFeira              _addNewFeiraUseCase;
        private readonly EditExistingFeiraParams  _editFeiraUseCase;
        private readonly RemoveExistingFeira      _removeFeiraUseCase;

        public FeiraController(
            ILogger<FeiraController> logger,
            IFeiraRepository         feiraRepository,
            AddNewFeira              addNewFeiraUseCase,
            EditExistingFeiraParams  editFeiraUseCase,
            RemoveExistingFeira      removeFeiraUseCase)
        {
            _logger             = logger;
            _feiraRepository    = feiraRepository;
            _addNewFeiraUseCase = addNewFeiraUseCase;
            _editFeiraUseCase   = editFeiraUseCase;
            _removeFeiraUseCase = removeFeiraUseCase;
        }

        [HttpGet(Name = "GetFeira")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}