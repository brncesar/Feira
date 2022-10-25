using FeirasLivres.Api.Controllers;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Feira.Api.Controllers
{
    public class DistritoController : BaseController
    {
        private readonly ILogger<DistritoController> _logger;
        private readonly IFeiraRepository            _feiraRepository;
        private readonly AddNewFeira                 _addNewFeiraUseCase;
        private readonly EditExistingFeiraParams     _editFeiraUseCase;
        private readonly RemoveExistingFeira         _removeFeiraUseCase;

        public DistritoController(
            ILogger<DistritoController> logger,
            IFeiraRepository            feiraRepository,
            AddNewFeira                 addNewFeiraUseCase,
            EditExistingFeiraParams     editFeiraUseCase,
            RemoveExistingFeira         removeFeiraUseCase)
        {
            _logger             = logger;
            _feiraRepository    = feiraRepository;
            _addNewFeiraUseCase = addNewFeiraUseCase;
            _editFeiraUseCase   = editFeiraUseCase;
            _removeFeiraUseCase = removeFeiraUseCase;
        }

        [HttpGet(Name = "GetFeiraByCode")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}