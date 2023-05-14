using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;

public class RemoveExistingFeira: IRemoveExistingFeira
{
    private readonly IFeiraRepository _feiraRepository;

    public RemoveExistingFeira(IFeiraRepository feiraRepsitory) => _feiraRepository = feiraRepsitory;

    public async Task<IDomainActionResult<bool>> Execute(RemoveExistingFeiraParams removeExistingFeiraParams)
    {
        var paramsValidationResult = new RemoveExistingFeiraParamsValidator().Validate(removeExistingFeiraParams);
        var removeExistingFeiraResult = new DomainActionResult<bool>(paramsValidationResult.Errors);

        if (paramsValidationResult.IsPropInvalid(removeExistingFeiraParams, p => p.NumeroRegistro))
            return removeExistingFeiraResult.SetValue(false);

        var removeFeiraRepositoryResult = await _feiraRepository.RemoveByNumeroRegistroAsync(removeExistingFeiraParams.NumeroRegistro);
        removeExistingFeiraResult.AddErrors(removeFeiraRepositoryResult.Errors);

        return removeExistingFeiraResult.SetValue(removeFeiraRepositoryResult.IsSuccess());
    }
}
