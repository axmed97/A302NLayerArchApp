using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public async Task CreateProductAsync(AddProductDTO model)
        {
            await using var context = new AppDbContext();
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                Product product = new()
                {
                    Discount = model.Discount,
                    IsStock = model.IsStock,
                    Price = model.Price,
                    Review = model.Review,
                };
                await context.Products.AddAsync(product);
                //await context.SaveChangesAsync();

                for (int i = 0; i < model.AddProductLanguageDTOs.Count; i++)
                {
                    ProductLanguage productLanguage = new()
                    {
                        ProductId = product.Id,
                        Name = model.AddProductLanguageDTOs[i].Name,
                        Description = model.AddProductLanguageDTOs[i].Description,
                        LangCode = model.AddProductLanguageDTOs[i].LangCode,
                    };
                    await context.AddAsync(productLanguage);
                }
                //await context.SaveChangesAsync();

                for (int i = 0; i < model.SubCategoryId.Count; i++)
                {
                    ProductSubCategory productSubCategory = new()
                    {
                        ProductId = product.Id,
                        SubCategoryId = model.SubCategoryId[i]
                    };
                    await context.AddAsync(productSubCategory);
                }
                //await context.SaveChangesAsync();

                foreach (var item in model.AddSpecificationDTOs)
                {
                    Specification specification = new();
                    specification.ProductId = product.Id;
                    await context.Specifications.AddAsync(specification);
                    //await context.SaveChangesAsync();
                    foreach (var specLang in item.AddSpecificationLanguageDTOs)
                    {
                        SpecificationLanguage specificationLanguage = new()
                        {
                            SpecificationId = specification.Id,
                            LangCode = specLang.LangCode,
                            Key = specLang.Key,
                            Value = specLang.Value
                        };
                        await context.SpecificationLanguages.AddAsync(specificationLanguage);
                    }
                }

                foreach (var item in model.AddProductPicturesDTOs)
                {
                    ProductPicture productPicture = new()
                    {
                        FileName = item.FileName,
                        Path = item.Path,
                        ProductId = product.Id
                    };
                    await context.ProductPictures.AddAsync(productPicture);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public GetProductDTO GetProduct(Guid id, string langCode)
        {
            using var context = new AppDbContext();

            var data = context.Products
                .Include(x => x.Specifications)
                .ThenInclude(x => x.SpecificationLanguages)
                .Include(x => x.ProductLanguages)
                .Include(x => x.ProductSubCategories)
                .ThenInclude(x => x.SubCategory)
                .FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                GetProductDTO model = new()
                {
                    LangCode = data.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).LangCode,
                    Description = data.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).Description,
                    IsStock = data.IsStock,
                    Discount = data.Discount,
                    Name = data.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).Name,
                    Price = data.Price,
                    Review = data.Review,
                    SubCategoryName = data.ProductSubCategories
                                .Where(x => x.ProductId == data.Id)
                                .Select(x => x.SubCategory.Name)
                                .ToList(),
                    GetSpecificationDTOs = data.Specifications.Select(x => new GetSpecificationDTO
                    {
                        Key = x.SpecificationLanguages.FirstOrDefault(y => y.LangCode == langCode).Key,
                        Value = x.SpecificationLanguages.FirstOrDefault(y => y.LangCode == langCode).Value
                    }).ToList()
                };
                return model;
            }
            return null;
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductDTO model)
        {
            await using var context = new AppDbContext();

            var data = context.Products
                .Include(x => x.Specifications)
                .Include(x => x.ProductLanguages)
                .Include(x => x.ProductSubCategories)
                .ThenInclude(x => x.SubCategory)
                .FirstOrDefault(x => x.Id == id);

            data.Review = model.Review;
            data.Discount = model.Discount;
            data.IsStock = model.IsStock;
            data.Price = model.Price;
            context.Update(data);

            context.RemoveRange(data.Specifications);
            context.RemoveRange(data.ProductLanguages);
            context.RemoveRange(data.ProductSubCategories);
            await context.SaveChangesAsync();

            foreach (var item in model.UpdateProductLanguageDTOs)
            {
                ProductLanguage productLanguage = new()
                {
                    Description = item.Description,
                    LangCode = item.LangCode,
                    Name = item.Name,
                    ProductId = data.Id,
                };
                await context.AddAsync(productLanguage);
            }
            await context.SaveChangesAsync();

            foreach (var item in model.UpdateSpecificationDTOs)
            {
                Specification specification = new();
                specification.ProductId = data.Id;
                await context.AddAsync(specification);
                foreach (var spec in item.UpdateSpecificationLanguageDTOs)
                {
                    SpecificationLanguage specificationLanguage = new()
                    {
                        Key = spec.Key,
                        Value = spec.Value,
                        SpecificationId = specification.Id,
                        LangCode = spec.LangCode
                    };
                    await context.AddAsync(specificationLanguage);
                }
                await context.SaveChangesAsync();
            }


            foreach (var item in model.SubCategoryId)
            {
                ProductSubCategory productSubCategory = new()
                {
                    ProductId = data.Id,
                    SubCategoryId = item
                };
                await context.AddAsync(productSubCategory);
            }
            await context.SaveChangesAsync();

        }
    }
}
