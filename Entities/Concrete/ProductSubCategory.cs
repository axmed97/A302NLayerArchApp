using Entities.Common;

namespace Entities.Concrete
{
    public class ProductSubCategory : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
