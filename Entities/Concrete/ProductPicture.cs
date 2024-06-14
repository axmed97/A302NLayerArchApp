namespace Entities.Concrete
{
    public class ProductPicture : Entities.Common.File
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
