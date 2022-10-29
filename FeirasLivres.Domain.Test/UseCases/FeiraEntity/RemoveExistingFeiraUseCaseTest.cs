using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;

namespace FeirasLivres.Domain.Test.UseCases.FeiraEntity;

public class RemoveExistingFeiraUseCaseTest
{
    private RemoveExistingFeira _testTarget;

    public RemoveExistingFeiraUseCaseTest(IFeiraRepository feiraRepsitory)
    {
        _testTarget = new RemoveExistingFeira(feiraRepsitory);
    }

    [Fact]
    public async Task MustReturnErrorWhenTryingRemoveInexistFeira()
    {
        var numeroRegistro = "0000-0";
        var useCaseParamObj = new RemoveExistingFeiraParams(numeroRegistro);

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        Assert.False(addNewFeiraResult.Value);
        Assert.Contains(addNewFeiraResult.Errors, err => err.Type == ErrorType.NotFound);
    }

    [Theory]
    [InlineData(      "")]
    [InlineData(     "0")]
    [InlineData(    "00")]
    [InlineData(   "000")]
    [InlineData(  "0000")]
    [InlineData( "00000")]
    [InlineData("000000")]
    [InlineData("0-0000")]
    [InlineData("00-000")]
    [InlineData("000-00")]
    [InlineData("abcd-e")]
    public async Task MustReturnErrorWhenTryingRemoveUsingInvalidNumeroRegistro(string numeroRegistro)
    {
        var useCaseParamObj = new RemoveExistingFeiraParams(numeroRegistro);

        var addNewFeiraResult = await _testTarget.Execute(useCaseParamObj);

        Assert.False(addNewFeiraResult.Value);
        Assert.Contains(addNewFeiraResult.Errors, err => err.Type == ErrorType.Validation);
    }
}