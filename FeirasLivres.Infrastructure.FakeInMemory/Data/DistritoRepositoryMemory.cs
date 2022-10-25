using ErrorOr;
using FeirasLivres.Domain.Common;
using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;

namespace FeirasLivres.Infrastructure.FakeInMemory.Data
{
    public class DistritoRepositoryMemory : IDistritoRepository
    {
        private List<Distrito> DistritosMock { get; }

        public DistritoRepositoryMemory()
        {
            DistritosMock = new List<Distrito>{
                new Distrito { Id = new Guid("9a3a04aa-5069-4ec6-86ff-7572b24e8f22"), Codigo = "01", Nome = "AGUA RASA"         },
                new Distrito { Id = new Guid("370b00a3-f45f-4693-b4bd-7a2918ca59b7"), Codigo = "18", Nome = "CANGAIBA"          },
                new Distrito { Id = new Guid("fa7e6176-cf00-4656-9779-ac4567c6845b"), Codigo = "02", Nome = "ALTO DE PINHEIROS" },
                new Distrito { Id = new Guid("d2278ca3-597b-447c-b9de-b3e1d1b7e9fd"), Codigo = "04", Nome = "ARICANDUVA"        },
                new Distrito { Id = new Guid("89373846-832f-4a38-a157-c3b73a541d74"), Codigo = "07", Nome = "AGUA"              },
            };
        }

        public async Task<IDomainActionResult<List<Distrito>>> GetAllAsync()
            => new DomainActionResult<List<Distrito>>(DistritosMock);

        public async Task<IDomainActionResult<Distrito>> GetByIdAsync(Guid id)
        {
            var distrito = DistritosMock.SingleOrDefault(f => f.Id == id);

            var domainRepositoryResult = new DomainActionResult<Distrito>(distrito);

            return distrito is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Distrito não encontratdo"));
        }

        public async Task<IDomainActionResult<Distrito>> GetByCodigoAsync(string codigo)
        {
            var distrito = DistritosMock.SingleOrDefault(f => f.Codigo == codigo.Trim());

            var domainRepositoryResult = new DomainActionResult<Distrito>(distrito);

            return distrito is not null
                ? domainRepositoryResult
                : domainRepositoryResult.AddError(ErrorHelpers.GetError(ErrorType.NotFound, "Distrito não encontratdo"));
        }
    }
}