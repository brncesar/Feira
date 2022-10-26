using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;

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
        IDomainActionResult<Distrito> resultGetDistritoByIdRepository;
        IDomainActionResult<SubPrefeitura> resultGetSubPrefeituraByIdRepository;
        var paramsValidationResult = new EditExistingFeiraParamsValidator().Validate(feiraInfosToEdit);
        var updateFeiraResult = new DomainActionResult<bool>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropValid(feiraInfosToEdit, p => p.CodDistrito))
        {
            resultGetDistritoByIdRepository = await _distritoRepository.GetByCodigoAsync(feiraInfosToEdit.CodDistrito);

            if (resultGetDistritoByIdRepository.HasErrors())
                updateFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());
            else
                feiraToSave.DistritoId = resultGetDistritoByIdRepository.Value.Id;
        }

        if (paramsValidationResult.IsPropValid(feiraInfosToEdit, p => p.CodSubPrefeitura))
        {
            resultGetSubPrefeituraByIdRepository = await _subPrefeituraRepository.GetByCodigoAsync(feiraInfosToEdit.CodSubPrefeitura);

            if (resultGetSubPrefeituraByIdRepository.HasErrors())
                updateFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());
            else
                feiraToSave.SubPrefeituraId = resultGetSubPrefeituraByIdRepository.Value.Id;
        }

        if (updateFeiraResult.HasErrors) return updateFeiraResult;

        var updateFeiraRepositoryResult = await _feiraRepository.UpdateByNumeroRegistroAsync(feiraInfosToEdit);
        updateFeiraResult.AddErrorsFrom(updateFeiraRepositoryResult);

        return updateFeiraResult.SetValue(updateFeiraRepositoryResult.IsSuccess());
    }
}
