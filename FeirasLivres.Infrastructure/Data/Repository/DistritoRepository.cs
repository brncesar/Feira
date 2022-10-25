using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class DistritoRepository : BaseRepository<FeirasLivresDbContext, Distrito>, IDistritoRepository
    {
        public DistritoRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }
    }
}