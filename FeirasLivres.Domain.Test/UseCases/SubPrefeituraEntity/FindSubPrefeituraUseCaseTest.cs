using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Test.UseCases.SubPrefeituraEntity
{
    public class FindSubPrefeituraUseCaseTest
    {
        private FindSubPrefeitura _testTarget;
        private FindSubPrefeituraParams _useCaseParamObj = new FindSubPrefeituraParams(
            Nome  : "AGUA",
            Codigo: "01");

        public FindSubPrefeituraUseCaseTest(FindSubPrefeitura findSubPrefeitura)
            => _testTarget = findSubPrefeitura;

        [Fact]
        public async Task MustReturnErrorWhenTryingToToFindWithoutAnyInformation()
        {
            var useCaseParamObj = new FindSubPrefeituraParams(null, null);

            var findSubPrefeituraResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(findSubPrefeituraResult.HasErrors());
        }

        [Fact]
        public async Task MustReturnEmptyListWhenDontFoundFeirasByFiltersParams()
        {
            var findSubPrefeituraResult = await _testTarget.Execute(_useCaseParamObj with { Codigo = "ab" });

            Assert.True(findSubPrefeituraResult.IsSuccess() && findSubPrefeituraResult.Value is not null && findSubPrefeituraResult.Value.None());
        }
    }
}