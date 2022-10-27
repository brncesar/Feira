using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Misc;
using FluentValidation;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

internal class FindFeiraParamsValidator : AbstractValidator<FindFeiraParams>
{
    public FindFeiraParamsValidator()
    {
        RuleFor(p => p.CodDistrito).Length  (2);
        RuleFor(p => p.Nome       ).Length  (3, 30);
        RuleFor(p => p.Bairro     ).Length  (2, 30);

        RuleFor(p => p.Regiao5    ).IsEnumName(typeof(Regiao5), caseSensitive: false)
            .WithMessage(p => $"Regiao5 '{p.Regiao5}' inválida. Os valores possíveis são: {string.Join(", ", Enum.GetValues(typeof(Regiao5)).Cast<Regiao5>())}");

        RuleFor(p => new { p.Bairro, p.Nome, p.Regiao5, p.CodDistrito })
            .Must(x =>
                x.Bairro     .IsNotNullOrNotEmpty() ||
                x.Nome       .IsNotNullOrNotEmpty() ||
                x.CodDistrito.IsNotNullOrNotEmpty() ||
                x.Regiao5    .IsNotNullOrNotEmpty())
            .WithMessage("É necessário informar pelo menos um parâmetro para busca da feira [ Distrito | Regiao5 | Nome da feira | Bairro ]");
    }
}