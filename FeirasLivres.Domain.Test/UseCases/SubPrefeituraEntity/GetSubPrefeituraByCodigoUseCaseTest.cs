using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Test.UseCases.SubPrefeituraEntity
{
    public class GetSubPrefeituraByCodigoUseCaseTest
    {
        private GetSubPrefeituraByCodigo _testTarget;

        public GetSubPrefeituraByCodigoUseCaseTest(GetSubPrefeituraByCodigo getSubPrefeituraByCodigo)
            => _testTarget = getSubPrefeituraByCodigo;

        [Fact]
        public async Task MustReturnErrorErrorWhenSubPrefeituraNotFound()
        {
            var useCaseParamObj = new GetSubPrefeituraByCodigoParams(Codigo: "00");
            var findSubPrefeituraResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(findSubPrefeituraResult.HasErrors());
        }

        [Fact]
        public async Task MustReturnErrorWhenCodParamIsInvalid()
        {
            var useCaseParamObj = new GetSubPrefeituraByCodigoParams(Codigo: "mustReturnError");
            var findSubPrefeituraResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(findSubPrefeituraResult.HasErrors());
        }
    }
}