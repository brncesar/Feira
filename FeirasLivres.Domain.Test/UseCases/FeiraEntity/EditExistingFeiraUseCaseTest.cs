using ErrorOr;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;

namespace FeirasLivres.Domain.Test.UseCases.FeiraEntity
{
    public class EditExistingFeiraUseCaseTest
    {
        private EditExistingFeira _testTarget;
        private EditExistingFeiraParams _useCaseParamObj = new EditExistingFeiraParams(
            Nome                 : "PIRASSUNUNGA",
            NumeroRegistro       : "1234-5",
            SetorCensitarioIBGE  : "355030801000054",
            AreaDePonderacaoIBGE : "3550308005039",
            Regiao5              : Regiao5.Leste,
            Regiao8              : Regiao8.Leste1,
            EnderecoLogradouro   : "RUA TEREZINA",
            EnderecoNumero       : "615",
            EnderecoBairro       : "ALTO DA MOOCA",
            EnderecoReferencia   : "CAMPO LARGO E MANAUS",
            Latitude             : 0,
            Longitude            : 0,
            DistritoId           : new Guid("d2278ca3-597b-447c-b9de-b3e1d1b7e9fd"),
            SubPrefeituraId      : new Guid("ab54e53a-0807-4d1e-8be9-412ae0cd7b2b")
        );

        public EditExistingFeiraUseCaseTest(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepository)
        {
            _testTarget = new EditExistingFeira(feiraRepsitory, distritoRepository, subPrefeituraRepository);
        }

        [Fact]
        public async Task MustReturnErrorWhenTryingRemoveInexistFeira()
        {
            var numeroRegistro  = "0000-0";
            var useCaseParamObj = _useCaseParamObj with {
                NumeroRegistro  = numeroRegistro
            };

            var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

            Assert.False(editExistingFeiraUseCaseResult.Value);
            Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
        }

        [Fact]
        public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedDistrito()
        {
            var invalidDistritoId = new Guid("00000000-0000-0000-0000-000000000000");
            var useCaseParamObj = _useCaseParamObj with
            {
                DistritoId = invalidDistritoId
            };

            var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(editExistingFeiraUseCaseResult.HasErrors());
            Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
        }

        [Fact]
        public async Task MustReturnErrorWhenTryingToAddAFeiraWithNoExistentRelatedSubPrefeitura()
        {
            var invalidSubPrefeituraId = new Guid("00000000-0000-0000-0000-000000000000");
            var useCaseParamObj = _useCaseParamObj with
            {
                SubPrefeituraId = invalidSubPrefeituraId
            };

            var editExistingFeiraUseCaseResult = await _testTarget.Execute(useCaseParamObj);

            Assert.True(editExistingFeiraUseCaseResult.HasErrors());
            Assert.Contains(editExistingFeiraUseCaseResult.Errors, err => err.Type == ErrorType.NotFound);
        }
    }
}