namespace Entities.DTOs.CategoryDTOs
{
    public class GetCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
}
