using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;
using FluentValidation.Results;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;

public class AddNewFeira
{
    private readonly IFeiraRepository         _feiraRepository;
    private readonly IDistritoRepository      _distritoRepository;
    private readonly ISubPrefeituraRepository _subPrefeituraRepository;

    public AddNewFeira(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepsitory)
        =>  (_feiraRepository, _distritoRepository, _subPrefeituraRepository) =
            ( feiraRepsitory ,  distritoRepository,  subPrefeituraRepsitory );

    public async Task<IDomainActionResult<AddNewFeiraResult>> Execute(AddNewFeiraParams newFeiraInfos)
    {
        var feiraToSave = new Feira();
        var paramsValidationResult = new AddNewFeiraParamsValidator().Validate(newFeiraInfos);
        var addNewFeiraResult = new DomainActionResult<AddNewFeiraResult>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropValid(newFeiraInfos, p => p.NumeroRegistro))
            await CheckIfTheFeiraDoesntExistAndAddErrorIfItExists(addNewFeiraResult, newFeiraInfos.NumeroRegistro);

        await TrySetRelatedDistritoIdOnFeiraObjOrAddDistritoNotFoundToDomainResult(
            feiraToSave,
            addNewFeiraResult,
            paramsValidationResult,
            newFeiraInfos);

        await TrySetRelatedSubPrefeituraIdOnFeiraObjOrAddSubPrefeituraNotFoundToDomainResult(
            feiraToSave,
            addNewFeiraResult,
            paramsValidationResult,
            newFeiraInfos);

        if (addNewFeiraResult.HasErrors) return addNewFeiraResult;

        MapValuesFormInParamsToFeiraObj(newFeiraInfos, feiraToSave);

        var addFeiraRepositoryResult = await _feiraRepository.AddAsync(feiraToSave);

        if (addFeiraRepositoryResult.IsSuccess())
            return addNewFeiraResult.SetValue(new AddNewFeiraResult(addFeiraRepositoryResult.Value));

        addNewFeiraResult.AddErrors(addFeiraRepositoryResult.Errors);

        return addNewFeiraResult;
    }

    private async Task TrySetRelatedDistritoIdOnFeiraObjOrAddDistritoNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<AddNewFeiraResult> addNewFeiraResult,
        ValidationResult paramsValidationResult,
        AddNewFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodDistrito)) return;

        var resultGetDistritoByCodRepository = await _distritoRepository.GetByCodigoAsync(newFeiraInfos.CodDistrito);

        if (resultGetDistritoByCodRepository.HasErrors())
            addNewFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());
        else
            feiraToSave.DistritoId = resultGetDistritoByCodRepository.Value.Id;
    }

    private async Task TrySetRelatedSubPrefeituraIdOnFeiraObjOrAddSubPrefeituraNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<AddNewFeiraResult> addNewFeiraResult,
        ValidationResult paramsValidationResult,
        AddNewFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodSubPrefeitura)) return;

        var resultGetSubPrefeituraByCodRepository = await _subPrefeituraRepository.GetByCodigoAsync(newFeiraInfos.CodSubPrefeitura);

        if (resultGetSubPrefeituraByCodRepository.HasErrors())
            addNewFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());
        else
            feiraToSave.SubPrefeituraId = resultGetSubPrefeituraByCodRepository.Value.Id;
    }

    private void MapValuesFormInParamsToFeiraObj(AddNewFeiraParams newFeiraInfos, Feira feiraToSave) {
        newFeiraInfos.MapValuesTo(ref feiraToSave);

        Enum.TryParse(newFeiraInfos.Regiao5, true, out Regiao5 enumRegiao5FromString);
        feiraToSave.Regiao5 = enumRegiao5FromString;

        Enum.TryParse(newFeiraInfos.Regiao8, true, out Regiao8 enumRegiao8FromString);
        feiraToSave.Regiao8 = enumRegiao8FromString;
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
                $"{existingFeiraReceivedFromRepository?.Nome} - {existingFeiraReceivedFromRepository?.NumeroRegistro}",
                $"{nameof(AddNewFeira)}.Execute"));
        }
        else if (unexpectedErrorHasReturned)
            getFeiraRepositoryResult.Errors.Where(err => err.Type is not ErrorType.NotFound).ToList().ForEach(err =>
                addNewFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err.Description)));
    }
}
