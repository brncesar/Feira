using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    public class AddNewFeira
    {
        private readonly IFeiraRepository         _feiraRepository;
        private readonly IDistritoRepository      _distritoRepository;
        private readonly ISubPrefeituraRepository _subPrefeituraRepository;

        public AddNewFeira(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepsitory)
            =>  (_feiraRepository, _distritoRepository, _subPrefeituraRepository) =
                ( feiraRepsitory ,  distritoRepository,  subPrefeituraRepsitory );

        public async Task<DomainActionResult<AddNewFeiraResult>> Execute(AddNewFeiraParams newFeiraInfos)
        {
            var paramsValidationResult = new AddNewFeiraParamsValidator().Validate(newFeiraInfos);
            var addNewFeiraResult = new DomainActionResult<AddNewFeiraResult>(paramsValidationResult.Errors);

            if (paramsValidationResult.IsPropValid(newFeiraInfos, p => p.NumeroRegistro))
                await CheckIfTheFeiraDoesntExistAndAddErrorIfItExists(addNewFeiraResult, newFeiraInfos.NumeroRegistro);

            if (await DistritoNotFound(newFeiraInfos.DistritoId))
                addNewFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());

            if (await SubPrefeituraNotFound(newFeiraInfos.SubPrefeituraId))
                addNewFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());

            if (addNewFeiraResult.HasErrors) return addNewFeiraResult;

            var feiraToSave = newFeiraInfos.MapValuesTo<Feira>();
            var addFeiraRepositoryResult = await _feiraRepository.AddAsync(feiraToSave);

            if (addFeiraRepositoryResult.IsSuccess())
                return new AddNewFeiraResult(addFeiraRepositoryResult.Value);

            addNewFeiraResult.AddErrors(addFeiraRepositoryResult.Errors);

            return addNewFeiraResult;
        }

        private async Task CheckIfTheFeiraDoesntExistAndAddErrorIfItExists(
            DomainActionResult<AddNewFeiraResult> addNewFeiraResult,
            string numeroRegistro)
        {
            var getFeiraRepositoryResult = await _feiraRepository.GetByNumeroRegistroAsync(numeroRegistro);

            var thisFeiraAlreadyExist = getFeiraRepositoryResult.Value is not null;
            var unexpectedErrorHasReturned = getFeiraRepositoryResult.Errors.Any(err => err.Type is not ErrorType.NotFound);

            if (thisFeiraAlreadyExist)
            {
                var existingFeiraReceivedFromRepository = getFeiraRepositoryResult.Value;

                addNewFeiraResult.AddError(AddNewFeiraErrors.DuplicateFeira(
                    $"Já existe uma outra feira cadastrada com este mesmo número de registro: " +
                    $"{existingFeiraReceivedFromRepository?.Nome} - {existingFeiraReceivedFromRepository?.NumeroRegistro}"));
            }
            else if (unexpectedErrorHasReturned)
                getFeiraRepositoryResult.Errors.Where(err => err.Type is not ErrorType.NotFound).ToList().ForEach(err =>
                    addNewFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err.Description)));
        }

        private async Task<bool> DistritoNotFound(Guid distritoId)
        {
            var resultGetDistritoByIdRepository = await _distritoRepository.GetByIdAsync(distritoId);
            return resultGetDistritoByIdRepository.HasErrors();
        }

        private async Task<bool> SubPrefeituraNotFound(Guid subPrefeituraId)
        {
            var resultGetSubPrefeituraByIdRepository = await _subPrefeituraRepository.GetByIdAsync(subPrefeituraId);
            return resultGetSubPrefeituraByIdRepository.HasErrors();
        }
    }
}
