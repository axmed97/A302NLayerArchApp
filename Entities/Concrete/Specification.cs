using Entities.Common;

namespace Entities.Concrete
{
    public class Specification : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<SpecificationLanguage> SpecificationLanguages { get; set; }
    }
}
