using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Test.UseCases.DistritoEntity;

public class FindDistritoUseCaseTest
{
    private FindDistrito _testTarget;
    private FindDistritoParams _useCaseParamObj = new FindDistritoParams(
        Nome  : "AGUA",
        Codigo: "01");

    public FindDistritoUseCaseTest(FindDistrito findDistrito) => _testTarget = findDistrito;

    [Fact]
    public async Task MustReturnErrorWhenTryingToFindWithoutAnyInformation()
    {
        var useCaseParamObj = new FindDistritoParams(null, null);

        var findDistritoResult = await _testTarget.Execute(useCaseParamObj);

        Assert.True(findDistritoResult.HasErrors());
    }

    [Fact]
    public async Task MustReturnEmptyListWhenDontFoundFeirasByFiltersParams()
    {
        var findDistritoResult = await _testTarget.Execute(_useCaseParamObj with { Codigo = "_test_" });

        Assert.True(findDistritoResult.IsSuccess() && findDistritoResult.Value is not null && findDistritoResult.Value.None());
    }
}