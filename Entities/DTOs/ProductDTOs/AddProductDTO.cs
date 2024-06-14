namespace Entities.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsStock { get; set; }
        public double Review { get; set; }
        public IList<Guid> SubCategoryId { get; set; }
        public List<AddProductLanguageDTO> AddProductLanguageDTOs { get; set; }
        public List<AddSpecificationDTO> AddSpecificationDTOs { get; set; }
        public List<AddProductPicturesDTO> AddProductPicturesDTOs { get; set; }
    }
}
