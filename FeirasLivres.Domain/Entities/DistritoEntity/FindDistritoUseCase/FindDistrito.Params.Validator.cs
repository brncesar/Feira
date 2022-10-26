using FeirasLivres.Domain.Misc;
using FluentValidation;

namespace FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

internal class FindDistritoParamsValidator : AbstractValidator<FindDistritoParams>
{
    public FindDistritoParamsValidator()
    {
        RuleFor(p => p.Codigo).Length(2, 9);
        RuleFor(p => p.Nome  ).Length(3,18);

        RuleFor(p => new { p.Nome, p.Codigo })
            .Must(x => x.Codigo.IsNotNullOrNotEmpty() || x.Nome.IsNotNullOrNotEmpty())
            .WithMessage(z => $"É necessário informar pelo menos um parâmetro para busca do Distrito [ Nome | Código ]");
    }
}