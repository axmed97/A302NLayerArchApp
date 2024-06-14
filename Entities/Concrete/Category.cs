using Entities.Common;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string PhotoUrl { get; set; }
        public ICollection<CategoryLanguage> CategoryLanguages { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
