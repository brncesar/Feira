using FeirasLivres.Domain.Misc;
using FluentValidation;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

internal class FindSubPrefeituraParamsValidator : AbstractValidator<FindSubPrefeituraParams>
{
    public FindSubPrefeituraParamsValidator()
    {
        RuleFor(p => p.Nome  ).Length(3, 25);
        RuleFor(p => p.Codigo).MaximumLength(2);

        RuleFor(p => new { p.Nome, p.Codigo })
            .Must(x => x.Codigo.IsNotNullOrNotEmpty() && x.Nome  .IsNotNullOrNotEmpty())
            .WithMessage("É necessário informar pelo menos um parâmetro para busca da Subprefeitura [ Nome | Código ]");
    }
}