using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.DistritoEntity
{
    public class Distrito : BaseEntity
    {
        public string Codigo { get; set; } = null!;
        public string Nome   { get; set; } = null!;
    }
}
