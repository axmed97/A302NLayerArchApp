using Entities.Common;

namespace Entities.Concrete
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
