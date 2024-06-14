namespace Entities.DTOs.BrandDTOs
{
    public class GetBrandDTO
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
    }
}
