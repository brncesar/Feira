using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;

public class GetSubPrefeituraByCodigo
{
    private readonly ISubPrefeituraRepository _distritoRepository;

    public GetSubPrefeituraByCodigo(ISubPrefeituraRepository distritoRepository)
        => _distritoRepository = distritoRepository;

    public async Task<IDomainActionResult<GetSubPrefeituraByCodigoResult>> Execute(GetSubPrefeituraByCodigoParams codeParameter)
    {
        var paramsValidationResult = new GetSubPrefeituraByCodigoParamsValidator().Validate(codeParameter);
        var getSubPrefeituraByCodigoResult = new DomainActionResult<GetSubPrefeituraByCodigoResult>(paramsValidationResult.Errors);

        if (paramsValidationResult.HasErrors())
            return getSubPrefeituraByCodigoResult;

        var respositoryResult = await _distritoRepository.GetByCodigoAsync(codeParameter.Codigo);

        if (respositoryResult.IsSuccess() && respositoryResult.Value is not null)
            return getSubPrefeituraByCodigoResult.SetValue(new(respositoryResult.Value.Codigo, respositoryResult.Value.Nome));
        else
            getSubPrefeituraByCodigoResult.AddNotFoundError($"{nameof(GetSubPrefeituraByCodigo)}.{nameof(Execute)}", "SubPrefeitura not found");

        return getSubPrefeituraByCodigoResult.AddErrors(respositoryResult.Errors.Where(err => err.Type != ErrorOr.ErrorType.NotFound).ToList());
    }
}
