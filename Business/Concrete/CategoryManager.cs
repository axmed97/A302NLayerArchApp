using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs.CategoryDTOs;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly ICategoryLanguageDAL _categoryLanguageDAL;

        public CategoryManager(ICategoryDAL categoryDAL, ICategoryLanguageDAL categoryLanguageDAL)
        {
            _categoryDAL = categoryDAL;
            _categoryLanguageDAL = categoryLanguageDAL;
        }

        public void Create(List<AddCategoryDTO> models)
        {
            _categoryDAL.CreateCategory(models);
        }

        public void Delete(Guid id)
        {
            _categoryDAL.DeleteCategory(id);
        }

        public List<GetCategoryDTO> GetAllByLang(string langCode)
        {
            var categories = _categoryLanguageDAL.GetAll(x => x.LangCode == langCode, false);
            var result = categories.Select(x => new GetCategoryDTO()
            {
                LangCode = x.LangCode,
                Name = x.Name,
                CategoryId = x.CategoryId,
            }).ToList();
            return result;
        }

        public GetCategoryDTO GetByLang(Guid id, string langCode)
        {
            var category = _categoryLanguageDAL.Get(x => x.CategoryId == id && x.LangCode == langCode, false);

            GetCategoryDTO getCategoryDTO = new()
            {
                Name = category.Name,
                CategoryId = id,
                LangCode = langCode
            };

            return getCategoryDTO;
        }

        public async Task Update(Guid id, List<UpdateCategoryDTO> models)
        {
            await _categoryDAL.UpdateCategoryAsync(id, models);
        }
    }
}
