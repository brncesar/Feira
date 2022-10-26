using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Test.UseCases.DistritoEntity
{
    public class GetDistritoByCodigoUseCaseTest
    {
        private GetDistritoByCodigo _testTarget;

        public GetDistritoByCodigoUseCaseTest(IDistritoRepository distritoRepository)
        {
            _testTarget = new GetDistritoByCodigo(distritoRepository);
        }

        [Fact]
        public async Task MustReturnErrorErrorWhenDistritoNotFound()
        {
            var useCaseParamObj = new GetDistritoByCodigoParams(Codigo: "00");
            var findDistritoResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(findDistritoResult.HasErrors());
        }

        [Fact]
        public async Task MustReturnErrorWhenCodParamIsInvalid()
        {
            var useCaseParamObj = new GetDistritoByCodigoParams(Codigo: "mustReturnError");
            var findDistritoResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(findDistritoResult.HasErrors());
        }
    }
}