using Entities.Common;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsStock { get; set; }
        public double Review { get; set; }
        public ICollection<ProductSubCategory> ProductSubCategories { get; set; }
        public ICollection<Specification> Specifications { get; set; }
        public ICollection<ProductLanguage> ProductLanguages { get; set; }
        public ICollection<ProductPicture> ProductPictures { get; set; }
    }
}
