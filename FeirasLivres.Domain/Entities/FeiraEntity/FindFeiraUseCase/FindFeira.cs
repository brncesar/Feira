using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public class FindFeira
{
    private readonly IFeiraRepository    _feiraRepository;
    private readonly IDistritoRepository _distritoRepository;

    public FindFeira(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository)
        => (_feiraRepository, _distritoRepository) = (feiraRepsitory ,distritoRepository);

    public async Task<IDomainActionResult<List<FeiraResult>>> Execute(FindFeiraParams findParameters)
    {
        var paramsValidationResult = new FindFeiraParamsValidator().Validate(findParameters);
        var findFeiraResult = new DomainActionResult<List<FeiraResult>>(paramsValidationResult.Errors);

        if (paramsValidationResult.HasErrors())
            return findFeiraResult;

        if (findParameters.CodDistrito is not null && await DistritoNotFound(findParameters.CodDistrito))
            return findFeiraResult.AddError(FindFeiraErrors.DistritoNotFound());

        var findFeirasRespositoryResult = await _feiraRepository.FindFeirasAsync(findParameters);

        if (findFeirasRespositoryResult.HasErrors())
            return findFeiraResult.AddErrors(findFeirasRespositoryResult.Errors);

        return findFeirasRespositoryResult.Value is not null
            ? findFeiraResult.SetValue(findFeirasRespositoryResult.Value)
            : findFeiraResult.SetValue(new());
    }

    private async Task<bool> DistritoNotFound(string codDistrito)
    {
        var resultGetDistritoByIdRepository = await _distritoRepository.GetByCodigoAsync(codDistrito);
        return resultGetDistritoByIdRepository.HasErrors();
    }
}
