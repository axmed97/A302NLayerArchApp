using Entities.Common;

namespace Entities.Concrete
{
    public class CategoryLanguage : BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
