using FeirasLivres.Domain.Misc;
using FluentValidation;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

internal class FindFeiraParamsValidator : AbstractValidator<FindFeiraParams>
{
    public FindFeiraParamsValidator()
    {
        RuleFor(p => p.Regiao5    ).IsInEnum();
        RuleFor(p => p.CodDistrito).Length  (2);
        RuleFor(p => p.Nome       ).Length  (3, 30);
        RuleFor(p => p.Bairro     ).Length  (2, 30);

        RuleFor(p => new { p.Bairro, p.Nome, p.Regiao5, p.CodDistrito })
            .Must(x =>
                x.Bairro     .IsNotNullOrNotEmpty() ||
                x.Nome       .IsNotNullOrNotEmpty() ||
                x.CodDistrito.IsNotNullOrNotEmpty() ||
                x.Regiao5 is not null)
            .WithMessage("É necessário informar pelo menos um parâmetro para busca da feira [ Distrito | Regiao5 | Nome da feira | Bairro ]");
    }
}