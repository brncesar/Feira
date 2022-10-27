using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;
using FluentValidation.Results;

namespace FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;

public class EditExistingFeira
{
    private readonly IFeiraRepository         _feiraRepository;
    private readonly IDistritoRepository      _distritoRepository;
    private readonly ISubPrefeituraRepository _subPrefeituraRepository;

    public EditExistingFeira(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepsitory)
        =>  (_feiraRepository, _distritoRepository, _subPrefeituraRepository) =
            ( feiraRepsitory ,  distritoRepository,  subPrefeituraRepsitory );

    public async Task<IDomainActionResult<bool>> Execute(EditExistingFeiraParams feiraInfosToEdit)
    {
        var feiraToSave = new Feira();
        var paramsValidationResult = new EditExistingFeiraParamsValidator().Validate(feiraInfosToEdit);
        var updateFeiraResult = new DomainActionResult<bool>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropValid(feiraInfosToEdit, p => p.NumeroRegistro))
            await CheckIfTheFeiraExistAndAddErrorIfDont(updateFeiraResult, feiraInfosToEdit.NumeroRegistro);

        await TrySetRelatedDistritoIdOnFeiraObjOrAddDistritoNotFoundToDomainResult(
            feiraToSave,
            updateFeiraResult,
            paramsValidationResult,
            feiraInfosToEdit);

        await TrySetRelatedSubPrefeituraIdOnFeiraObjOrAddSubPrefeituraNotFoundToDomainResult(
            feiraToSave,
            updateFeiraResult,
            paramsValidationResult,
            feiraInfosToEdit);

        if (updateFeiraResult.HasErrors) return updateFeiraResult;

        MapValuesFormInParamsToFeiraObj(feiraInfosToEdit, feiraToSave);

        var updateFeiraRepositoryResult = await _feiraRepository.UpdateByNumeroRegistroAsync(feiraInfosToEdit);
        updateFeiraResult.AddErrorsFrom(updateFeiraRepositoryResult);

        return updateFeiraResult.SetValue(updateFeiraRepositoryResult.IsSuccess());
    }

    private async Task TrySetRelatedDistritoIdOnFeiraObjOrAddDistritoNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<bool> updateFeiraResult,
        ValidationResult paramsValidationResult,
        EditExistingFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodDistrito)) return;

        var resultGetDistritoByCodRepository = await _distritoRepository.GetByCodigoAsync(newFeiraInfos.CodDistrito);

        if (resultGetDistritoByCodRepository.HasErrors())
            updateFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());
        else
            feiraToSave.DistritoId = resultGetDistritoByCodRepository.Value.Id;
    }

    private async Task TrySetRelatedSubPrefeituraIdOnFeiraObjOrAddSubPrefeituraNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<bool> updateFeiraResult,
        ValidationResult paramsValidationResult,
        EditExistingFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodSubPrefeitura)) return;

        var resultGetSubPrefeituraByCodRepository = await _subPrefeituraRepository.GetByCodigoAsync(newFeiraInfos.CodSubPrefeitura);

        if (resultGetSubPrefeituraByCodRepository.HasErrors())
            updateFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());
        else
            feiraToSave.SubPrefeituraId = resultGetSubPrefeituraByCodRepository.Value.Id;
    }

    private void MapValuesFormInParamsToFeiraObj(EditExistingFeiraParams feiraInfosToEdit, Feira feiraToSave)
    {
        feiraInfosToEdit.MapValuesTo(ref feiraToSave);

        Enum.TryParse(feiraInfosToEdit.Regiao5, true, out Regiao5 enumRegiao5FromString);
        feiraToSave.Regiao5 = enumRegiao5FromString;

        Enum.TryParse(feiraInfosToEdit.Regiao8, true, out Regiao8 enumRegiao8FromString);
        feiraToSave.Regiao8 = enumRegiao8FromString;
    }

    private async Task CheckIfTheFeiraExistAndAddErrorIfDont(
        DomainActionResult<bool> addNewFeiraResult,
        string numeroRegistro)
    {
        var getFeiraRepositoryResult = await _feiraRepository.GetByNumeroRegistroAsync(numeroRegistro);

        if (getFeiraRepositoryResult.Value is not null) return;

        addNewFeiraResult.AddError(EditExistingFeiraErrors.FeiraNotFound(numeroRegistro));

        getFeiraRepositoryResult.Errors
            .Where(err => err.Type is not ErrorType.NotFound).ToList()
            .ForEach(err => addNewFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err.Description)));
    }
}
