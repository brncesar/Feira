using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;

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
        IDomainActionResult<Distrito> resultGetDistritoByIdRepository;
        IDomainActionResult<SubPrefeitura> resultGetSubPrefeituraByIdRepository;
        var paramsValidationResult = new AddNewFeiraParamsValidator().Validate(newFeiraInfos);
        var addNewFeiraResult = new DomainActionResult<AddNewFeiraResult>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropValid(newFeiraInfos, p => p.NumeroRegistro))
            await CheckIfTheFeiraDoesntExistAndAddErrorIfItExists(addNewFeiraResult, newFeiraInfos.NumeroRegistro);

        if (paramsValidationResult.IsPropValid(newFeiraInfos, p => p.CodDistrito))
        {
            resultGetDistritoByIdRepository = await _distritoRepository.GetByCodigoAsync(newFeiraInfos.CodDistrito);

            if (resultGetDistritoByIdRepository.HasErrors())
                addNewFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());
            else
                feiraToSave.DistritoId = resultGetDistritoByIdRepository.Value.Id;
        }

        if (paramsValidationResult.IsPropValid(newFeiraInfos, p => p.CodSubPrefeitura))
        {
            resultGetSubPrefeituraByIdRepository = await _subPrefeituraRepository.GetByCodigoAsync(newFeiraInfos.CodSubPrefeitura);

            if(resultGetSubPrefeituraByIdRepository.HasErrors())
                addNewFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());
            else
                feiraToSave.SubPrefeituraId = resultGetSubPrefeituraByIdRepository.Value.Id;
        }

        if (addNewFeiraResult.HasErrors) return addNewFeiraResult;

        newFeiraInfos.MapValuesTo(ref feiraToSave);
        var addFeiraRepositoryResult = await _feiraRepository.AddAsync(feiraToSave);

        if (addFeiraRepositoryResult.IsSuccess())
            return addNewFeiraResult.SetValue(new AddNewFeiraResult(addFeiraRepositoryResult.Value));

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
}
