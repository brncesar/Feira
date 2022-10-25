using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Infrastructure.FakeInMemory.Data
{
    public class FeiraRepositoryMemory : IFeiraRepository
    {
        private List<Feira> FeirasMock { get; }

        public FeiraRepositoryMemory()
        {
            FeirasMock = new List<Feira>{
                new Feira (){
                    Nome                 = "PIRASSUNUNGA",
                    NumeroRegistro       = "1013-8",
                    SetorCensitarioIBGE  = "355030801000054",
                    AreaDePonderacaoIBGE = "3550308005039",
                    Regiao5              = Regiao5.Leste,
                    Regiao8              = Regiao8.Leste1,
                    EnderecoLogradouro   = "RUA TEREZINA",
                    EnderecoNumero       = "615",
                    EnderecoBairro       = "ALTO DA MOOCA",
                    EnderecoReferencia   = "CAMPO LARGO E MANAUS",
                    Latitude             = -23564711,
                    Longitude            = -23564711,
                    DistritoId           = new Guid("9a3a04aa-5069-4ec6-86ff-7572b24e8f22"),
                    Distrito             = new Distrito      { Id = new Guid("9a3a04aa-5069-4ec6-86ff-7572b24e8f22"), Codigo = "01", Nome = "AGUA RASA"},
                    SubPrefeituraId      = new Guid("2335194b-95e3-4ce1-9bc8-f9cc3c09943d"),
                    SubPrefeitura        = new SubPrefeitura { Id = new Guid("2335194b-95e3-4ce1-9bc8-f9cc3c09943d"), Codigo = "25", Nome = "MOOCA"    },
                },
                new Feira (){
                    Nome                 = "ENGENHEIRO GOULART",
                    NumeroRegistro       = "5041-5",
                    SetorCensitarioIBGE  = "355030818000024",
                    AreaDePonderacaoIBGE = "3550308005137",
                    Regiao5              = Regiao5.Leste,
                    Regiao8              = Regiao8.Leste1,
                    EnderecoLogradouro   = "RUA DR AUGUSTO S LOPES",
                    EnderecoNumero       = "200",
                    EnderecoBairro       = "ENG GOULART",
                    EnderecoReferencia   = "RUA H MARQUES E M DOS SANTOS",
                    Latitude             = -23498151,
                    Longitude            = -46515902,
                    DistritoId           = new Guid("370b00a3-f45f-4693-b4bd-7a2918ca59b7"),
                    Distrito             = new Distrito      { Id = new Guid("370b00a3-f45f-4693-b4bd-7a2918ca59b7"), Codigo = "18", Nome = "CANGAIBA"},
                    SubPrefeituraId      = new Guid("f87ef259-70cd-417e-bab8-7fb404a811a0"),
                    SubPrefeitura        = new SubPrefeitura { Id = new Guid("f87ef259-70cd-417e-bab8-7fb404a811a0"), Codigo = "21", Nome = "PENHA"   },
                },
                new Feira (){
                    Nome                 = "VILA IDA",
                    NumeroRegistro       = "5060-1",
                    SetorCensitarioIBGE  = "355030802000009",
                    AreaDePonderacaoIBGE = "3550308005106",
                    Regiao5              = Regiao5.Oeste,
                    Regiao8              = Regiao8.Oeste,
                    EnderecoLogradouro   = "RUA REALENGO",
                    EnderecoBairro       = "VL IDA",
                    EnderecoReferencia   = "TV RUA CAMINHA DO AMORIM",
                    Latitude             = -23543154,
                    Longitude            = -46703775,
                    DistritoId           = new Guid("fa7e6176-cf00-4656-9779-ac4567c6845b"),
                    Distrito             = new Distrito      { Id = new Guid("fa7e6176-cf00-4656-9779-ac4567c6845b"), Codigo = "02", Nome = "ALTO DE PINHEIROS" },
                    SubPrefeituraId      = new Guid("a5fb3e06-702b-43ce-8ff8-7bc70a00c810"),
                    SubPrefeitura        = new SubPrefeitura { Id = new Guid("a5fb3e06-702b-43ce-8ff8-7bc70a00c810"), Codigo = "11", Nome = "PINHEIROS"         },
                },
                new Feira (){
                    Nome                 = "JARDIM IVA",
                    NumeroRegistro       = "6108-5",
                    SetorCensitarioIBGE  = "355030804000110",
                    AreaDePonderacaoIBGE = "3550308005151",
                    Regiao5              = Regiao5.Leste,
                    Regiao8              = Regiao8.Leste1,
                    EnderecoLogradouro   = "RUA ROMILDO FINOZZI",
                    EnderecoNumero       = "21",
                    EnderecoBairro       = "JD IVA",
                    EnderecoReferencia   = "ES BARREIRA GRANDE ALTURA 3120",
                    Latitude             = -23586503,
                    Longitude            = -46515555,
                    DistritoId           = new Guid("d2278ca3-597b-447c-b9de-b3e1d1b7e9fd"),
                    Distrito             = new Distrito      { Id = new Guid("d2278ca3-597b-447c-b9de-b3e1d1b7e9fd"), Codigo = "04", Nome = "ARICANDUVA"                },
                    SubPrefeituraId      = new Guid("ab54e53a-0807-4d1e-8be9-412ae0cd7b2b"),
                    SubPrefeitura        = new SubPrefeitura { Id = new Guid("ab54e53a-0807-4d1e-8be9-412ae0cd7b2b"), Codigo = "26", Nome = "ARICANDUVA-FORMOSA-CARRAO" },
                },
                new Feira (){
                    Nome                 = "AGUA FUNDA",
                    NumeroRegistro       = "3036-8",
                    SetorCensitarioIBGE  = "355030827000059",
                    AreaDePonderacaoIBGE = "3550308005091",
                    Regiao5              = Regiao5.Sul,
                    Regiao8              = Regiao8.Sul1,
                    EnderecoLogradouro   = "RUA MARIO SCHIOPA",
                    EnderecoNumero       = "175",
                    EnderecoBairro       = "AGUA FUNDA",
                    EnderecoReferencia   = "SIDERURGICA ALIPERTI",
                    Latitude             = -23631217,
                    Longitude            = -46624008,
                    DistritoId           = new Guid("89373846-832f-4a38-a157-c3b73a541d74"),
                    Distrito             = new Distrito      { Id = new Guid("89373846-832f-4a38-a157-c3b73a541d74"), Codigo = "07", Nome = "AGUA"    },
                    SubPrefeituraId      = new Guid("cb2db6d5-a2bd-4fc4-9e4a-c6b8941cb6ac"),
                    SubPrefeitura        = new SubPrefeitura { Id = new Guid("cb2db6d5-a2bd-4fc4-9e4a-c6b8941cb6ac"), Codigo = "13", Nome = "IPIRANGA"},
                },
            };
        }

        public async Task<IDomainActionResult<Feira>> GetByIdAsync(Guid id)
        {
            var feira = GetFeiraById(id);

            var domainRepositoryResult = new DomainActionResult<Feira>(feira);

            return feira is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));
        }

        public async Task<IDomainActionResult<bool>> UpdateAsync(Feira feiraToUpdate)
        {
            int indexItemToUpdate = FeirasMock.FindIndex(f => f.Id == feiraToUpdate.Id);

            if (indexItemToUpdate != -1)
                return new DomainActionResult<bool>(false)
                    .AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));

            FeirasMock[indexItemToUpdate] = feiraToUpdate;

            return new DomainActionResult<bool>(true);
        }

        public async Task<IDomainActionResult<bool>> DeleteAsync(Guid id)
        {
            var feiraToDelete = GetFeiraById(id);

            if (feiraToDelete is null)
                return new DomainActionResult<bool>(false)
                    .AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));

            var successOnDelete = FeirasMock.Remove(feiraToDelete);

            return successOnDelete
                ? new DomainActionResult<bool>(true)
                : new DomainActionResult<bool>(false)
                    .AddError(ErrorHelpers.GetError(ErrorType.Unexpected, "Erro inesperado. Não foi possível excluir a feira"));
        }

        public async Task<IDomainActionResult<Feira>> GetByNumeroRegistroAsync(string numeroRegistro)
        {
            var feira = GetFeiraByNumeroRegistro(numeroRegistro);

            var domainRepositoryResult = new DomainActionResult<Feira>(feira);

            return feira is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));
        }

        public async Task<IDomainActionResult<bool>> RemoveByNumeroRegistroAsync(string numeroRegistro)
        {
            var feiraToDelete = GetFeiraByNumeroRegistro(numeroRegistro);

            var domainRepositoryResult = new DomainActionResult<bool>(false);

            if (feiraToDelete is null)
                return domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));

            var successOnDelete = FeirasMock.Remove(feiraToDelete);

            return successOnDelete
                ? domainRepositoryResult.SetValue(true)
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.Unexpected, "Erro inesperado. Não foi possível excluir a feira"));
        }

        public async Task<IDomainActionResult<Guid>> AddAsync(Feira feira)
        {
            feira.Id = Guid.NewGuid();

            FeirasMock.Add(feira);

            return new DomainActionResult<Guid>(feira.Id);
        }

        public async Task<IDomainActionResult<List<Feira>>> GetAllAsync()
        {
            return new DomainActionResult<List<Feira>>(FeirasMock);
        }

        public async Task<IDomainActionResult<bool>> UpdateByNumeroRegistroAsync(EditExistingFeiraParams paramFeiraToUpdate)
        {
            var domainRepositoryResult = new DomainActionResult<bool>(false);

            var repositoryFeiraToUpdate = GetFeiraByNumeroRegistro(paramFeiraToUpdate.NumeroRegistro);

            if (repositoryFeiraToUpdate is null)
                return domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Feira não encontrada"));

            paramFeiraToUpdate.MapValuesTo(ref repositoryFeiraToUpdate);

            int indexItemToUpdate = FeirasMock.FindIndex(f => f.Id == repositoryFeiraToUpdate.Id);
            FeirasMock[indexItemToUpdate] = repositoryFeiraToUpdate;

            return domainRepositoryResult.SetValue(true);
        }

        private Feira? GetFeiraByNumeroRegistro(string numeroRegistro)
            => FeirasMock.SingleOrDefault(f => f.NumeroRegistro == numeroRegistro.Trim());

        private Feira? GetFeiraById(Guid Id)
            => FeirasMock.SingleOrDefault(f => f.Id == Id);
    }
}
