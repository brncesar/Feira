using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

public class FindDistrito
{
    private readonly IDistritoRepository _distritoRepository;

    public FindDistrito(IDistritoRepository distritoRepository)
        => _distritoRepository = distritoRepository;

    public async Task<IDomainActionResult<List<FindDistritoResult>>> Execute(FindDistritoParams findParameters)
    {
        var paramsValidationResult = new FindDistritoParamsValidator().Validate(findParameters);
        var findFeiraResult = new DomainActionResult<List<FindDistritoResult>>(paramsValidationResult.Errors);

        // if (paramsValidationResult.HasErrors())
            return findFeiraResult;/*

        if (findParameters.CodDistrito is not null && await DistritoNotFound(findParameters.CodDistrito))
            return findFeiraResult.AddError(FindDistritoErrors.DistritoNotFound());

        var findFeirasRespositoryResult = await _distritoRepository.FindFeirasAsync(findParameters);

        if (findFeirasRespositoryResult.HasErrors())
            return findFeiraResult.AddErrors(findFeirasRespositoryResult.Errors);

        return findFeirasRespositoryResult.Value is not null
            ? findFeiraResult.SetValue(findFeirasRespositoryResult.Value)
            : findFeiraResult.SetValue(new());*/
    }

    private async Task<bool> DistritoNotFound(string codDistrito)
    {
        var resultGetDistritoByIdRepository = await _distritoRepository.GetByCodigoAsync(codDistrito);
        return resultGetDistritoByIdRepository.HasErrors();
    }
}
