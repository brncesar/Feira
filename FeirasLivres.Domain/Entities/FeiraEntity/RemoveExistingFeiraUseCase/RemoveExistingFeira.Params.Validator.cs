using FluentValidation;

namespace FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase
{
    public class RemoveExistingFeiraParamsValidator : AbstractValidator<RemoveExistingFeiraParams>
    {
        public RemoveExistingFeiraParamsValidator()
        {
            RuleFor(p => p.NumeroRegistro)
                .Matches("[0-9]{4}[-][0-9]")
                .WithMessage(p =>
                    $"O número do registro da feira deve ser informado no " +
                    $"formato ####-#. O valor informado foi: {p.NumeroRegistro}");
        }
    }
}