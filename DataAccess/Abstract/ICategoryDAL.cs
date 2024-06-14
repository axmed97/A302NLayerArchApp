using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        void CreateCategory(List<AddCategoryDTO> models);
        Task UpdateCategoryAsync(Guid id, List<UpdateCategoryDTO> models);
        void DeleteCategory(Guid id);
        GetCategoryDTO GetCategoryByLangCode(Guid id, string langCode);
        List<GetCategoryDTO> GetAllCategories(string langCode = "az");
    }
}
