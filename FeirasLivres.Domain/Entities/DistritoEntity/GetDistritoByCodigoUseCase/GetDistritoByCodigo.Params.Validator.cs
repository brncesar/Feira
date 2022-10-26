using FluentValidation;

namespace FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;

internal class GetDistritoByCodigoParamsValidator : AbstractValidator<GetDistritoByCodigoParams>
{
    public GetDistritoByCodigoParamsValidator()
    {
        RuleFor(p => p.Codigo).Length(2, 9);
    }
}