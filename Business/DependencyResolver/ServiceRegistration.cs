using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Mapping;
using Business.Utilities.Storage.Abstract;
using Business.Utilities.Storage.Concrete;
using Business.Validations;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            // IoC  -  Inversion of Control container
            services.AddScoped<AppDbContext>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            services.AddScoped<ICategoryLanguageDAL, EFCategoryLanguageDAL>();

            services.AddScoped<ISubCategoryService, SubCategoryManager>();
            services.AddScoped<ISubCategoryDAL, EFSubCategoryDAL>();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();

            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IBrandDAL, EFBrandDAL>();

            services.AddScoped<IStorageService, StorageService>();

            ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddStorageService<T>(this IServiceCollection services)
            where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
