using System.Text.Json.Serialization;

namespace Entities.DTOs.BrandDTOs
{
    public class UpdateBrandDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
    }
}
