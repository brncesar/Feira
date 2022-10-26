using FluentValidation;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;

internal class GetSubPrefeituraByCodigoParamsValidator : AbstractValidator<GetSubPrefeituraByCodigoParams>
{
    public GetSubPrefeituraByCodigoParamsValidator()
    {
        RuleFor(p => p.Codigo).Length(1, 2);
    }
}