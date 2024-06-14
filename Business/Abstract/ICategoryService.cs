using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void Create(List<AddCategoryDTO> models);
        Task Update(Guid id, List<UpdateCategoryDTO> models);
        void Delete(Guid id);
        GetCategoryDTO GetByLang(Guid id, string langCode);
        List<GetCategoryDTO> GetAllByLang(string langCode);
    }
}
