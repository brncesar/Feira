using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.Common;
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

    public async Task<IDomainActionResult<FeiraResult>> Execute(EditExistingFeiraParams feiraInfosToEdit)
    {
        var feiraToSave = new Feira();
        var paramsValidationResult = new EditExistingFeiraParamsValidator().Validate(feiraInfosToEdit);
        var updateFeiraResult = new DomainActionResult<FeiraResult>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropValid(feiraInfosToEdit, p => p.NumeroRegistro))
            feiraToSave = await GetExistingFeiraOrAddErrorIfDontExist(updateFeiraResult, feiraInfosToEdit.NumeroRegistro);

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

        var updateFeiraRepositoryResult = await _feiraRepository.UpdateByNumeroRegistroAsync(feiraToSave);

        return updateFeiraRepositoryResult.IsSuccess()
            ? updateFeiraResult.SetValue (feiraToSave.ToFeiraResult())
            : updateFeiraResult.AddErrors(updateFeiraRepositoryResult.Errors);
    }

    private async Task TrySetRelatedDistritoIdOnFeiraObjOrAddDistritoNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<FeiraResult> updateFeiraResult,
        ValidationResult paramsValidationResult,
        EditExistingFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodDistrito)) return;

        if (newFeiraInfos.CodDistrito is null && feiraToSave.DistritoId != default(Guid))
        {
            await LoadRelatedDistrito(feiraToSave);
            return;
        }

        var resultGetDistritoByCodRepository = await _distritoRepository.GetByCodigoAsync(newFeiraInfos.CodDistrito);

        if (resultGetDistritoByCodRepository.HasErrors())
            updateFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());
        else
        {
            feiraToSave.Distrito   = resultGetDistritoByCodRepository.Value;
            feiraToSave.DistritoId = resultGetDistritoByCodRepository.Value.Id;
        }
    }

    private async Task TrySetRelatedSubPrefeituraIdOnFeiraObjOrAddSubPrefeituraNotFoundToDomainResult(
        Feira feiraToSave,
        DomainActionResult<FeiraResult> updateFeiraResult,
        ValidationResult paramsValidationResult,
        EditExistingFeiraParams newFeiraInfos)
    {
        if (paramsValidationResult.IsPropInvalid(newFeiraInfos, p => p.CodSubPrefeitura)) return;

        if (newFeiraInfos.CodSubPrefeitura is null && feiraToSave.SubPrefeituraId != default(Guid))
        {
            await LoadRelatedSubPrefeitura(feiraToSave);
            return;
        }

        var resultGetSubPrefeituraByCodRepository = await _subPrefeituraRepository.GetByCodigoAsync(newFeiraInfos.CodSubPrefeitura);

        if (resultGetSubPrefeituraByCodRepository.HasErrors())
            updateFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());
        else
        {
            feiraToSave.SubPrefeitura   = resultGetSubPrefeituraByCodRepository.Value;
            feiraToSave.SubPrefeituraId = resultGetSubPrefeituraByCodRepository.Value.Id;
        }
    }

    private void MapValuesFormInParamsToFeiraObj(EditExistingFeiraParams feiraInfosToEdit, Feira feiraToSave)
    {
        feiraInfosToEdit.MapValuesTo(ref feiraToSave, ignoreNullValues: true);

        if (feiraInfosToEdit.Regiao5 is not null)
        {
            Enum.TryParse(feiraInfosToEdit.Regiao5, true, out Regiao5 enumRegiao5FromString);
            feiraToSave.Regiao5 = enumRegiao5FromString;
        }

        if (feiraInfosToEdit.Regiao8 is not null)
        {
            Enum.TryParse(feiraInfosToEdit.Regiao8, true, out Regiao8 enumRegiao8FromString);
            feiraToSave.Regiao8 = enumRegiao8FromString;
        }
    }

    private async Task<Feira> GetExistingFeiraOrAddErrorIfDontExist(
        DomainActionResult<FeiraResult> updateFeiraResult,
        string numeroRegistro)
    {
        var getFeiraRepositoryResult = await _feiraRepository.GetByNumeroRegistroAsync(numeroRegistro);
        var feiraFromRepository = getFeiraRepositoryResult.Value;

        if (feiraFromRepository is not null) return feiraFromRepository;

        updateFeiraResult.AddError(EditExistingFeiraErrors.FeiraNotFound(numeroRegistro));

        getFeiraRepositoryResult.Errors
            .Where(err => err.Type is not ErrorType.NotFound).ToList()
            .ForEach(err => updateFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err.Description)));

        return new();
    }

    private async Task<Feira> LoadRelatedDistrito(Feira feira)
    {
        var getDistritoRepositoryResponse = await _distritoRepository.GetByIdAsync(feira.DistritoId);
        feira.Distrito = getDistritoRepositoryResponse.Value;
        return feira;
    }

    private async Task<Feira> LoadRelatedSubPrefeitura(Feira feira)
    {
        var getSubPrefeituraRepositoryResponse = await _subPrefeituraRepository.GetByIdAsync(feira.SubPrefeituraId);
        feira.SubPrefeitura = getSubPrefeituraRepositoryResponse.Value;
        return feira;
    }
}
