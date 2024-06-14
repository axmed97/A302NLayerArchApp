using Entities.Common;

namespace Entities.Concrete
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
    }
}
