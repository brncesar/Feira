using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity;

namespace FeirasLivres.Domain.Entities.DistritoEntity
{
    public class Distrito : BaseEntity
    {
        public string      Codigo { get; set; } = null!;
        public string      Nome   { get; set; } = null!;
        public List<Feira> Feiras { get; set; } = new();
    }
}
