using Entities.Common;

namespace Entities.Concrete
{
    public class SpecificationLanguage : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string LangCode { get; set; }
        public Guid SpecificationId { get; set; }
        public Specification Specification { get; set; }
    }
}
