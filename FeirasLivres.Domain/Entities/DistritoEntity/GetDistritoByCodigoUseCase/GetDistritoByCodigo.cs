using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;

public class GetDistritoByCodigo
{
    private readonly IDistritoRepository _distritoRepository;

    public GetDistritoByCodigo(IDistritoRepository distritoRepository)
        => _distritoRepository = distritoRepository;

    public async Task<IDomainActionResult<GetDistritoByCodigoResult>> Execute(GetDistritoByCodigoParams codeParameter)
    {
        var paramsValidationResult = new GetDistritoByCodigoParamsValidator().Validate(codeParameter);
        var getDistritoByCodigoResult = new DomainActionResult<GetDistritoByCodigoResult>(paramsValidationResult.Errors);

        if (paramsValidationResult.HasErrors())
            return getDistritoByCodigoResult;

        var respositoryResult = await _distritoRepository.GetByCodigoAsync(codeParameter.Codigo);

        if (respositoryResult.IsSuccess() && respositoryResult.Value is not null)
            return getDistritoByCodigoResult.SetValue(new(respositoryResult.Value.Codigo, respositoryResult.Value.Nome));
        else
            getDistritoByCodigoResult.AddNotFoundError($"{nameof(GetDistritoByCodigo)}.{nameof(Execute)}", "Distrito not found");

        return getDistritoByCodigoResult.AddErrors(respositoryResult.Errors.Where(err => err.Type != ErrorType.NotFound).ToList());
    }
}
