using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

public class FindDistrito : IFindDistrito
{
    private readonly IDistritoRepository _distritoRepository;

    public FindDistrito(IDistritoRepository distritoRepository)
        => _distritoRepository = distritoRepository;

    public async Task<IDomainActionResult<List<FindDistritoResult>>> Execute(FindDistritoParams findParameters)
    {
        var paramsValidationResult = new FindDistritoParamsValidator().Validate(findParameters);
        var findDistritoResult = new DomainActionResult<List<FindDistritoResult>>(paramsValidationResult.Errors);

        if (paramsValidationResult.HasErrors())
            return findDistritoResult;

        var findDistritosRespositoryResult = await _distritoRepository.FindDistritosAsync(findParameters);

        if (findDistritosRespositoryResult.HasErrors())
            return findDistritoResult.AddErrors(findDistritosRespositoryResult.Errors);

        return findDistritosRespositoryResult.Value is not null
            ? findDistritoResult.SetValue(findDistritosRespositoryResult.Value)
            : findDistritoResult.SetValue(new());
    }
}
