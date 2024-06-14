using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;



// todo async
// todo ValidationAntiForgeryToken
namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public void CreateCategory(List<AddCategoryDTO> models)
        {
            using var context = new AppDbContext();

            Category category = new();
            context.Categories.Add(category);
            context.SaveChanges();

            for (int i = 0; i < models.Count; i++)
            {
                CategoryLanguage categoryLanguage = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CreatedDate = DateTime.Now,
                    CategoryId = category.Id
                };
                context.CategoryLanguages.Add(categoryLanguage);
            }
            context.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            using var context = new AppDbContext();

            var category = context.Categories.AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            var categoryLanguage = context.CategoryLanguages.Where(x => x.CategoryId == id);
            context.Categories.Remove(category);
            context.CategoryLanguages.RemoveRange(categoryLanguage);
            context.SaveChanges();
        }

        public List<GetCategoryDTO> GetAllCategories(string langCode = "az")
        {
            using var context = new AppDbContext();
            var categories = context.CategoryLanguages.AsNoTracking()
                .Where(x => x.LangCode == langCode).ToList();

            var results = categories.Select(x => new GetCategoryDTO()
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                LangCode = x.LangCode
            }).ToList();

            return results;
        }

        public GetCategoryDTO GetCategoryByLangCode(Guid id, string langCode)
        {
            using var context = new AppDbContext();

            var category = context.CategoryLanguages.AsNoTracking()
                .FirstOrDefault(x => x.CategoryId == id && x.LangCode == langCode);

            GetCategoryDTO getCategoryDTO = new()
            {
                Name = category.Name,
                LangCode = langCode,
                CategoryId = id
            };

            return getCategoryDTO;
        }

        public async Task UpdateCategoryAsync(Guid id, List<UpdateCategoryDTO> models)
        {
            using var context = new AppDbContext();

            var category = context.Categories.FirstOrDefault(x => x.Id == id);

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            var categoryLanguage = context.CategoryLanguages
                .Where(x => x.CategoryId == category.Id)
                .ToList();

            context.CategoryLanguages.RemoveRange(categoryLanguage);

            for (int i = 0; i < models.Count; i++)
            {
                CategoryLanguage newCategoryLanguage = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CategoryId = category.Id
                };
                await context.CategoryLanguages.AddAsync(newCategoryLanguage);
            }
            await context.SaveChangesAsync();
        }
    }
}
