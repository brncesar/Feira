using FluentValidation;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    internal class AddNewFeiraParamsValidator : AbstractValidator<AddNewFeiraParams>
    {
        public AddNewFeiraParamsValidator()
        {
            RuleFor(p => p.Regiao5             ).IsInEnum     ();
            RuleFor(p => p.Regiao8             ).IsInEnum     ();
            RuleFor(p => p.EnderecoNumero      ).MaximumLength(5);
            RuleFor(p => p.EnderecoReferencia  ).MaximumLength(24);
            RuleFor(p => p.AreaDePonderacaoIBGE).Length       (13);
            RuleFor(p => p.Nome                ).Length       (3, 30);
            RuleFor(p => p.EnderecoBairro      ).Length       (2, 30);
            RuleFor(p => p.CodDistrito         ).NotEmpty     ().MaximumLength(9);
            RuleFor(p => p.CodSubPrefeitura    ).NotEmpty     ().MaximumLength(2);

            RuleFor(p => p.NumeroRegistro).Matches("[0-9]{4}[-][0-9]")
                .WithMessage(p =>
                    $"O número do registro da feira deve ser informado no formato ####-#. " +
                    $"O valor informado foi: {p.NumeroRegistro}");

            RuleFor(p => p.SetorCensitarioIBGE).Length(15)
                .WithMessage(p =>
                    $"O setor censitário deve ser informado com exatamente 15 caracteres. " +
                    $"O valor informado foi: {p.SetorCensitarioIBGE} ({p.SetorCensitarioIBGE.Length} caracteres)");

            RuleFor(p => p.EnderecoLogradouro).Length(3, 34)
                .WithMessage(p =>
                    $"O logradouro deve ter pelo menos 3 caracteres e no máximo 34. " +
                    $"O logradouro informado foi: {p.EnderecoLogradouro}");

            RuleFor(p => p.Latitude).Must(BeAValidLatitude)
                .WithMessage(p =>
                    $"Latitude inválida. O valor deve estar compreendido entre -90 e 90." +
                    $"O valor informado foi: {p.Latitude}");

            RuleFor(p => p.Longitude).Must(BeAValidLongitude)
                .WithMessage(p =>
                    $"Longitude inválida. O valor deve estar compreendido entre -180 e 180." +
                    $"O valor informado foi: {p.Longitude}");
        }

        private bool BeAValidLatitude (double latitude ) => latitude  is >=  -90 and <=  90;
        private bool BeAValidLongitude(double longitude) => longitude is >= -180 and <= 180;
    }
}