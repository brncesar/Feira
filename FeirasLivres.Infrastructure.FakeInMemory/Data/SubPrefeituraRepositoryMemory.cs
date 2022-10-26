using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Infrastructure.FakeInMemory.Data
{
    public class SubPrefeituraRepositoryMemory : ISubPrefeituraRepository
    {
        private List<SubPrefeitura> SubPrefeiturasMock { get; }

        public SubPrefeituraRepositoryMemory()
        {
            SubPrefeiturasMock = new List<SubPrefeitura>{
                new SubPrefeitura { Id = new Guid("2335194b-95e3-4ce1-9bc8-f9cc3c09943d"), Codigo = "25", Nome = "MOOCA"                     },
                new SubPrefeitura { Id = new Guid("f87ef259-70cd-417e-bab8-7fb404a811a0"), Codigo = "21", Nome = "PENHA"                     },
                new SubPrefeitura { Id = new Guid("a5fb3e06-702b-43ce-8ff8-7bc70a00c810"), Codigo = "11", Nome = "PINHEIROS"                 },
                new SubPrefeitura { Id = new Guid("ab54e53a-0807-4d1e-8be9-412ae0cd7b2b"), Codigo = "26", Nome = "ARICANDUVA-FORMOSA-CARRAO" },
                new SubPrefeitura { Id = new Guid("cb2db6d5-a2bd-4fc4-9e4a-c6b8941cb6ac"), Codigo = "13", Nome = "IPIRANGA"                  },
            };
        }

        public async Task<IDomainActionResult<List<SubPrefeitura>>> GetAllAsync()
            => new DomainActionResult<List<SubPrefeitura>>(SubPrefeiturasMock);

        public async Task<IDomainActionResult<SubPrefeitura>> GetByIdAsync(Guid id)
        {
            var subPrefeitura = SubPrefeiturasMock.SingleOrDefault(f => f.Id == id);

            var domainRepositoryResult = new DomainActionResult<SubPrefeitura>(subPrefeitura);

            return subPrefeitura is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddNotFoundError($"{nameof(SubPrefeituraRepositoryMemory)}.{nameof(GetByIdAsync)}", "Subprefeitura não encontrada");
        }

        public async Task<IDomainActionResult<SubPrefeitura>> GetByCodigoAsync(string codigo)
        {
            var subPrefeitura = SubPrefeiturasMock.SingleOrDefault(f => f.Codigo == codigo.Trim());

            var domainRepositoryResult = new DomainActionResult<SubPrefeitura>(subPrefeitura);

            return subPrefeitura is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddNotFoundError($"{nameof(SubPrefeituraRepositoryMemory)}.{nameof(GetByCodigoAsync)}", "Subprefeitura não encontrada");
        }

        public async Task<IDomainActionResult<List<FindSubPrefeituraResult>>> FindSubPrefeiturasAsync(FindSubPrefeituraParams findParams)
        {
            var domainActionResult = new DomainActionResult<List<FindSubPrefeituraResult>>();

            var listResult = SubPrefeiturasMock;
            var subPrefeiturasResult = new List<FindSubPrefeituraResult>();

            if (findParams.Nome.IsNotNullOrNotEmpty())
                listResult = listResult.Where(db => db.Nome.Contains(findParams.Nome.Trim())).ToList();

            if (findParams.Codigo.IsNotNullOrNotEmpty())
                listResult = listResult.Where(db => db.Codigo == findParams.Codigo).ToList();

            listResult.ForEach(feiraEntity => subPrefeiturasResult.Add(new(
                Nome  : feiraEntity.Nome,
                Codigo: feiraEntity.Codigo)));

            return domainActionResult.SetValue(subPrefeiturasResult);
        }
    }
}