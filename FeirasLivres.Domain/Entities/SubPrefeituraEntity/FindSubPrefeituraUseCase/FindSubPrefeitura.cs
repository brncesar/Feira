using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

public class FindSubPrefeitura
{
    private readonly ISubPrefeituraRepository _distritoRepository;

    public FindSubPrefeitura(ISubPrefeituraRepository distritoRepository)
        => _distritoRepository = distritoRepository;

    public async Task<IDomainActionResult<List<FindSubPrefeituraResult>>> Execute(FindSubPrefeituraParams findParameters)
    {
        var paramsValidationResult = new FindSubPrefeituraParamsValidator().Validate(findParameters);
        var findSubPrefeituraResult = new DomainActionResult<List<FindSubPrefeituraResult>>(paramsValidationResult.Errors);

        if (paramsValidationResult.HasErrors())
            return findSubPrefeituraResult;

        var findSubPrefeiturasRespositoryResult = await _distritoRepository.FindSubPrefeiturasAsync(findParameters);

        if (findSubPrefeiturasRespositoryResult.HasErrors())
            return findSubPrefeituraResult.AddErrors(findSubPrefeiturasRespositoryResult.Errors);

        return findSubPrefeiturasRespositoryResult.Value is not null
            ? findSubPrefeituraResult.SetValue(findSubPrefeiturasRespositoryResult.Value)
            : findSubPrefeituraResult.SetValue(new());
    }
}
