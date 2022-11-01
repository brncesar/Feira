using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Test.UseCases.FeiraEntity;

public class AddNewFeiraUseCaseTest
{
    private AddNewFeira _testTarget;
    private AddNewFeiraParams _useCaseParamObj = new AddNewFeiraParams(
        Nome                 : "PIRASSUNUNGA",
        NumeroRegistro       : "1234-5",
        SetorCensitarioIBGE  : "355030801000054",
        AreaDePonderacaoIBGE : "3550308005039",
        Regiao5              : "Leste",
        Regiao8              : "Leste1",
        EnderecoLogradouro   : "RUA TEREZINA",
        EnderecoNumero       : "615",
        EnderecoBairro       : "ALTO DA MOOCA",
        EnderecoReferencia   : "CAMPO LARGO E MANAUS",
        Latitude             : 0,
        Longitude            : 0,
        CodDistrito          : "01",
        CodSubPrefeitura     : "25"
    );

    public AddNewFeiraUseCaseTest(AddNewFeira addNewFeira) => _testTarget = addNewFeira;

    [Theory]
    [InlineData(-91,    0, false)]
    [InlineData(-85,  181, false)]
    [InlineData(-98,  274, false)]
    [InlineData(-90, -180, true )]
    [InlineData( 90,  180, true )]
    [InlineData( 45,   90, true )]
    public async Task MustReturnErrorWhenTheCoordinatesArentValids(double latitude, double longitude, bool isValid)
    {
        var useCaseParamObj = _useCaseParamObj with
        {
            Latitude  = latitude,
            Longitude = longitude
        };

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        var errs = addNewFeiraResult.Errors;

        var isLatitudeValid  = addNewFeiraResult.Errors.None( errs => errs.Description.Contains(nameof(useCaseParamObj.Latitude )) );
        var isLongitudeValid = addNewFeiraResult.Errors.None( errs => errs.Description.Contains(nameof(useCaseParamObj.Longitude)) );

        var isCoordinatesValids = isLatitudeValid && isLongitudeValid;

        Assert.Equal(expected: isValid, actual: isCoordinatesValids);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingToAddAFeiraThatAlreadyHaveTheSameNumeroRegistro()
    {
        var numeroRegistro = "0000-0";
        var useCaseParamObj = _useCaseParamObj with
        {
            NumeroRegistro = numeroRegistro
        };

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(addNewFeiraResult.IsSuccess());

        var anotherNewFeira = await _testTarget.Execute(useCaseParamObj);

        Assert.True(anotherNewFeira.HasErrors());
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedDistrito()
    {
        var invalidCodDistrito = "00000";
        var useCaseParamObj = _useCaseParamObj with
        {
            CodDistrito = invalidCodDistrito
        };

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(addNewFeiraResult.HasErrors());
        Assert.Contains(addNewFeiraResult.Errors, err => err.Type == ErrorType.NotFound);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedSubPrefeitura()
    {
        var invalidCodSubPrefeitura = "ab";
        var useCaseParamObj = _useCaseParamObj with
        {
            CodSubPrefeitura = invalidCodSubPrefeitura
        };

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(addNewFeiraResult.HasErrors());
        Assert.Contains(addNewFeiraResult.Errors, err => err.Type == ErrorType.NotFound);
    }
}