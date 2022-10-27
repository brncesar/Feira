using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;

namespace FeirasLivres.Domain.Test.UseCases.FeiraEntity;

public class EditExistingFeiraUseCaseTest
{
    private EditExistingFeira _testTarget;
    private EditExistingFeiraParams _useCaseParamObj = new EditExistingFeiraParams(
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
        CodSubPrefeitura     : "25");

    public EditExistingFeiraUseCaseTest(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepository)
    {
        _testTarget = new EditExistingFeira(feiraRepsitory, distritoRepository, subPrefeituraRepository);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingEditInexistFeira()
    {
        var numeroRegistro  = "0000-0";
        var useCaseParamObj = _useCaseParamObj with {
            NumeroRegistro  = numeroRegistro
        };

        var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

        Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedDistrito()
    {
        var invalidCodDistrito = "00000";
        var useCaseParamObj = _useCaseParamObj with
        {
            CodDistrito = invalidCodDistrito
        };

        var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(editExistingFeiraUseCaseResult.HasErrors());
        Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedSubPrefeitura()
    {
        var invalidCodSubPrefeitura = "ab";
        var useCaseParamObj = _useCaseParamObj with
        {
            CodSubPrefeitura = invalidCodSubPrefeitura
        };

        var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(editExistingFeiraUseCaseResult.HasErrors());
        Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
    }
}