using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity
{
    public class SubPrefeitura : BaseEntity
    {
        public string Codigo { get; set; } = null!;
        public string Nome   { get; set; } = null!;
    }
}
