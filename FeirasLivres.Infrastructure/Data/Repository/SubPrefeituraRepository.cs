using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;

namespace FeirasLivres.Infrastructure.Data.Repository
{
    public class SubPrefeituraRepository : BaseRepository<FeirasLivresDbContext, SubPrefeitura>, ISubPrefeituraRepository
    {
        public SubPrefeituraRepository(FeirasLivresDbContext dbCtx) : base(dbCtx) { }
    }
}