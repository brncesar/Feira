using FeirasLivres.Domain.Misc;
using FluentValidation;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

internal class FindSubPrefeituraParamsValidator : AbstractValidator<FindSubPrefeituraParams>
{
    public FindSubPrefeituraParamsValidator()
    {
        RuleFor(p => p.Codigo).MaximumLength(2);
    }
}